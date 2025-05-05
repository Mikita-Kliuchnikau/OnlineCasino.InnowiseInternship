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
        var connectionString = configuration.GetConnectionString("UsersDbContext");

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlServerActions =>
            {
                int MaxRetryCount = Convert.ToInt32(configuration.GetSection("DatabaseOptions").GetRequiredSection("MaxRetryCount").Value);
                sqlServerActions.EnableRetryOnFailure(MaxRetryCount);

                int CommandTimeout = Convert.ToInt32(configuration.GetSection("DatabaseOptions").GetRequiredSection("CommandTimeout").Value);
                sqlServerActions.CommandTimeout(CommandTimeout);
            });
            options.AddInterceptors(new TimestampInterceptor());
        });
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}

