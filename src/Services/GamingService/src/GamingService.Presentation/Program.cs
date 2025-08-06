using GamingService.Presentation.DI;
using GamingService.Presentation.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.UseExceptionMiddleware();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();
