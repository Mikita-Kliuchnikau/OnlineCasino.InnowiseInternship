using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagementService.DAL.Interfaces;
using UsersManagementService.DAL.Interceptors;
using UsersManagementService.DAL.Repositories;
using UsersManagementService.DAL.Context;

namespace UsersManagementService.DAL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        const string ConfigurationConnectionString = "UsersDbContext";
        const string ConfigurationDatabaseConnection = "DatabaseOptions";
        const string ConfigurationRetryCountOnFilure = "MaxRetryCount";
        const string ConfigurationCommandTimeout = "CommandTimeout";

        var connectionString = configuration.GetConnectionString(ConfigurationConnectionString);

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlServerActions =>
            {
                var MaxRetryCount = Convert.ToInt32(configuration
                    .GetSection(ConfigurationDatabaseConnection)
                    .GetRequiredSection(ConfigurationRetryCountOnFilure)
                    .Value);

                sqlServerActions.EnableRetryOnFailure(MaxRetryCount);

                var CommandTimeout = Convert.ToInt32(configuration
                    .GetSection(ConfigurationDatabaseConnection)
                    .GetRequiredSection(ConfigurationCommandTimeout)
                    .Value);

                sqlServerActions.CommandTimeout(CommandTimeout);
            });
            options.AddInterceptors(new TimestampInterceptor());
            options.AddInterceptors(new SoftDeleteInterceptor());
        });
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();
        return services;
    }
}                   

