using UsersManagementService.DAL.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDAL(builder.Configuration);

var app = builder.Build();

app.Run();
