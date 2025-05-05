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
        var connectionString = configuration.GetConnectionString(nameof(UsersDbContext))
            ?? throw new InvalidOperationException("Connection string nameof \"UsersDbContext\" not found.");

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlServerActions =>
            {
                sqlServerActions.EnableRetryOnFailure(3);

                sqlServerActions.CommandTimeout(5);
            });
            options.AddInterceptors(new TimestampInterceptor());
        });
        services.AddScoped<IUsersDbContext, UsersDbContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}

