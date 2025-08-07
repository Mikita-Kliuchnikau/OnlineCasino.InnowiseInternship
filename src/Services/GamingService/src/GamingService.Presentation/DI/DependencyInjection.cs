using GamingService.Application.DI;
using GamingService.DataAccess.DI;
using GamingService.Mapping;
using GamingService.Presentation.Options;
using System.Reflection;

namespace GamingService.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.ConfigureOptions<Auth0OptionsSetup>();

        services.AddApplication();
        services.AddDataAccess();

        return services;
    }
}
