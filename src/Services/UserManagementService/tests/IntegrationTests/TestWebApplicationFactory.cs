using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;
using Respawn;
using System.Data.Common;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using UsersManagementService.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Testcontainers.Azurite;
using static UsersManagementService.Common.Constants.Environments;
using static UsersManagementService.IntegrationTests.Constants.EnvironmentVariables;
using static UsersManagementService.IntegrationTests.Constants.EndpointsUrls;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using UsersManagementService.Common.Constants;
namespace UsersManagementService.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:17")
        .WithDatabase("usersdb")
        .WithUsername("postgres")
        .WithPassword("password")
        .Build();

    private readonly AzuriteContainer _blobContainer = new AzuriteBuilder()
        .WithImage("mcr.microsoft.com/azure-storage/azurite:latest")
        .WithName("azurite.blob-storage")
        .WithPortBinding(10000, 10000)
        .Build();

    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;
    private string? _auth0ClientSecret = string.Empty;
    public HttpClient HttpClient { get; private set; } = null!;

    public readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable(UsersDatabaseKey, _dbContainer.GetConnectionString());
        Environment.SetEnvironmentVariable(AzureBlobStorageKey, _blobContainer.GetConnectionString());
        Environment.SetEnvironmentVariable(AspNetCoreEnvironment, TestEnvironment);

        builder.ConfigureTestServices(services =>
        {
            services.EnsureDbCreated<UsersDbContext>();
        });
    }

    public async Task InitializeAsync()
    {
        Console.WriteLine($"[DEBUG] AUTH0_TEST_CLIENT_SECRET: {Environment.GetEnvironmentVariable(EnvironmentAuth0ClientSecretKey)}");
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
        await _dbContainer.StartAsync(cts.Token);
        await _blobContainer.StartAsync(cts.Token);

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        GetAuth0ClientSecretFronConfiguration();
        
        HttpClient = CreateClient();

        await SetAccessToken(HttpClient, cts.Token);

        await _dbConnection.OpenAsync();

        await InitializeRespawnerAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _dbConnection.DisposeAsync();
        await _blobContainer.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    private async Task InitializeRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            WithReseed = true
        });
    }

    private async Task SetAccessToken(HttpClient client, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[DEBUG] AUTH0_TEST_CLIENT_SECRET: {Environment.GetEnvironmentVariable(EnvironmentAuth0ClientSecretKey)}");
        using var httpClient = new HttpClient();
        var requestUri = Auth0TestTokenUrl;
        var requestBody = new
        {
            client_id = Auth0ClientId,
            client_secret = string.IsNullOrEmpty(_auth0ClientSecret) 
                ? Environment.GetEnvironmentVariable(EnvironmentAuth0ClientSecretKey)
                : _auth0ClientSecret,
            audience = Auth0ApiAudience,
            grant_type = "client_credentials"
        };
        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            System.Text.Encoding.UTF8,
            MediaTypeConstants.Json
        );
        var response = await httpClient.PostAsync(requestUri, content, cancellationToken);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(json);
        var accessToken = doc.RootElement.GetProperty("access_token").GetString();
        var tokenType = doc.RootElement.GetProperty("token_type").GetString();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType!, accessToken);
    }

    private void GetAuth0ClientSecretFronConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppsettingsKey, optional: true)
            .AddUserSecrets<Program>(optional: true)
            .Build();
        _auth0ClientSecret = configuration[Auth0ClientSecretKey];
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection EnsureDbCreated<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.Migrate();
        return services;
    }
}
