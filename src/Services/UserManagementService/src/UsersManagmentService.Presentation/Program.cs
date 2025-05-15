using UsersManagmentService.Presentation.Middleware;
using UsersManagementService.BLL.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBLL(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware();

await app.RunAsync();
