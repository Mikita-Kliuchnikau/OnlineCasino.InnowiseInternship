using UsersManagmentService.Presentation.Middleware;
using UsersManagementService.DAL.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDAL(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware();

app.Run();
