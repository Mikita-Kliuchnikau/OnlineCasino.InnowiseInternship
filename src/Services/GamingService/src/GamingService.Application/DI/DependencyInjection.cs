using GamingService.Application.Common.Mapping;
using GamingService.Core.Abstractions;
using GamingService.Core.Events;
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
        services.AddScoped<IDomainEventPublisher, MediatorDomainEventPublisher>();
        return services;
    }
}
