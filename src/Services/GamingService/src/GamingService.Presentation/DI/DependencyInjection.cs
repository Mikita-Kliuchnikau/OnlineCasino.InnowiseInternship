using GamingService.Application.DI;
using GamingService.DataAccess.DI;
using GamingService.Mapping;
using System.Reflection;

namespace GamingService.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddDataAccess();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        return services;
    }
}
