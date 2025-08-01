using GamingService.Core.Abstractions;
using GamingService.DataAccess.Options;
using GamingService.DataAccess.Repositories;
using GamingService.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace GamingService.DataAccess.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddSingleton<IMongoClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var clientOptions = new MongoClientSettings
            {
                Server = new MongoServerAddress(options.HostName, options.Port),
                Credential = MongoCredential.CreateCredential(options.CredentialName, options.CredentialUser, options.CredentialPassword),
                ReplicaSetName = options.ReplicaSetName
            };
            return new MongoClient(clientOptions);
        });

        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IRouletteConfiguratonsRepository, RouletteConfigurationsRepository>();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        return services;
    }
}