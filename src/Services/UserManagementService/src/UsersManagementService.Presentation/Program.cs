using UsersManagementService.Presentation.DI;
using UsersManagementService.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
public partial class Program { }