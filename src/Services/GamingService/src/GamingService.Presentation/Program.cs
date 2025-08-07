using GamingService.Presentation.DI;
using GamingService.Presentation.Extensions;
using GamingService.Presentation.Middleware;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();

builder.Services.AddSwagger();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddJwtAuthentication();

var app = builder.Build();

app.UseRequestLogContextMiddleware();
app.UseExceptionMiddleware();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

await app.RunAsync();
