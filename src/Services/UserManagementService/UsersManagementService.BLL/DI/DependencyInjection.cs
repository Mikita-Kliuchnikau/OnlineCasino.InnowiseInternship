using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Reflection.Metadata;
using UsersManagementService.BLL.Behaviors;
using UsersManagementService.BLL.Interfaces;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.BLL.Models.User.Commands.CreateUser;
using UsersManagementService.BLL.Models.User.Commands.DeleteUser;
using UsersManagementService.BLL.Models.User.Commands.UpdateUser;
using UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;
using UsersManagementService.BLL.Models.User.Queries.GetUser;
using UsersManagementService.BLL.Services;
using UsersManagementService.DAL.DI;

namespace UsersManagementService.BLL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDAL(configuration);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IImagesService, ImagesService>();
        return services;
    }
}