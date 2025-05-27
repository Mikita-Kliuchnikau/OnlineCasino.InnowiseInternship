using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.MappingConfigurations;
using UsersManagementService.BLL.Models.User.MappingConfigurations;
using UsersManagementService.BLL.Services;
using UsersManagementService.BLL.Services.Decorators;
using UsersManagementService.BLL.Validators.ImagesValidators;
using UsersManagementService.BLL.Validators.UsersValidators;
using UsersManagementService.DAL.DI;

namespace UsersManagementService.BLL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDAL(configuration);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddUsersMappingConfig();
        services.AddImagesMappingConfig();

        services.AddScoped<IUsersValidator, UsersValidator>();
        services.AddScoped<IImagesValidator, ImagesValidator>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IImagesService, ImagesService>();
        services.Decorate<IUsersService, UsersServiceValidationDecorator>();
        services.Decorate<IImagesService, ImagesServiceValidationDecorator>();
        return services;
    }
}