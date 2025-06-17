using Serilog;
using System.Text.Json.Serialization;
using UsersManagementService.Presentation.DI;
using UsersManagementService.Presentation.Extensions;
using UsersManagementService.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwagger();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddJwtAuthentication();

builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

app.MapControllers();

app.UseRequestLogContextMiddleware();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

await app.RunAsync();
public partial class Program { }