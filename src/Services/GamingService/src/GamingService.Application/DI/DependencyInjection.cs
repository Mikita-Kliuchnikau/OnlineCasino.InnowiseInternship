using GamingService.Application.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GamingService.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });
        return services;
    }
}
