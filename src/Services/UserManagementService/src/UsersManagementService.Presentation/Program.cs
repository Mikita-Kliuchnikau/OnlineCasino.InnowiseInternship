using Serilog;
using UsersManagementService.Presentation.DI;
using UsersManagementService.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

var azureAccountConnectionString = Environment.GetEnvironmentVariable("AZURE_ACCOUNT_CONNECTION_STRING");
if (!string.IsNullOrEmpty(azureAccountConnectionString))
{
    builder.Configuration["ConnectionStrings:AzureBlobStorage"] = azureAccountConnectionString;
}

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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