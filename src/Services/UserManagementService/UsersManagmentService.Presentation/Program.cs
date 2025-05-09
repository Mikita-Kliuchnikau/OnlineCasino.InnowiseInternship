using UsersManagmentService.Presentation.Middleware;
using UsersManagementService.BLL.DI;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBLL(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware();

app.Run();
