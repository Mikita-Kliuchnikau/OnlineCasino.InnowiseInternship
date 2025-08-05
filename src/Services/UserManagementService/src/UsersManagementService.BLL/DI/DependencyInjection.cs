using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Interceptors;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.MappingConfigurations;
using UsersManagementService.BLL.Models.User.MappingConfigurations;
using UsersManagementService.BLL.Services;
using UsersManagementService.BLL.Validators;
using UsersManagementService.DAL.DI;
using IValidatorFactory = UsersManagementService.BLL.Interfaces.Validators.IValidatorFactory;

namespace UsersManagementService.BLL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDAL(configuration);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddUsersMappingConfig();
        services.AddImagesMappingConfig();

        services.AddScoped<IValidatorFactory, ValidatorFactory>();
        services.AddScoped<ValidationInterceptor>();
        services.AddScopedProxyServer<UsersService, IUsersService>();
        services.AddScopedProxyServer<ImagesService, IImagesService>();
        services.AddSingleton<IMessageDeduplicationService, MessageDeduplicationService>();
        return services;
    }
}