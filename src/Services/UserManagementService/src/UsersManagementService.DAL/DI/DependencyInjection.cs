using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagementService.DAL.Interceptors;
using UsersManagementService.DAL.Repositories;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Options;
using Microsoft.Extensions.Options;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.DAL.Interfaces.Services;
using UsersManagementService.DAL.Services;

namespace UsersManagementService.DAL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();
        services.ConfigureOptions<BlobStorageOptionsSetup>();

        services.AddDbContext<UsersDbContext>((serviceProvider ,options) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
            options.UseNpgsql(databaseOptions.ConnectionString, sqlServerActions =>
            {
                sqlServerActions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerActions.CommandTimeout(databaseOptions.CommandTimeOut);
            });

            options.AddInterceptors(new TimestampInterceptor());
            options.AddInterceptors(new SoftDeleteInterceptor());
        });
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();
        services.AddSingleton<IAzureBlobService, AzureBlobService>();
        return services;
    }
}