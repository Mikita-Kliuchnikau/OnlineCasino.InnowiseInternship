using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using System.Net;
using System.Text.Json.Serialization;
using UsersManagementService.Common.Constants;
using UsersManagementService.Presentation.AuthScopes;
using UsersManagementService.Presentation.DI;
using UsersManagementService.Presentation.Extensions;
using UsersManagementService.Presentation.gRPC.Services;
using UsersManagementService.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5010, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.ListenAnyIP(5015, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwagger();

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddJwtAuthentication();

builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

app.UseMiddleware();

app.MapControllers();
app.MapGrpcService<UsersGrpcService>();

app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();
public partial class Program { }