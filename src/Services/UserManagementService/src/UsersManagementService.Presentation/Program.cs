using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Text.Json.Serialization;
using UsersManagementService.Common.Constants;
using UsersManagementService.Presentation.AuthScopes;
using UsersManagementService.Presentation.DI;
using UsersManagementService.Presentation.Extensions;
using UsersManagementService.Presentation.gRPC.Services;
using UsersManagementService.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

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

app.MapWhen(
    context => !context.Request.ContentType?.StartsWith(MediaTypeConstants.Grpc) ?? false,
    appBuilder =>
    {
        app.UseExceptionMiddleware();
        app.UseRequestLogContextMiddleware();
        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();
    }
);
app.MapControllers();
app.MapGrpcService<UsersGrpcService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
public partial class Program { }