using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using UsersManagementService.BLL.Interceptors;

namespace UsersManagementService.BLL.Extensions;

public static class DIExtention
{
    public static IServiceCollection AddScopedProxyServer<TService, TInterface>(
        this IServiceCollection services)
        where TService : class, TInterface
        where TInterface : class
    {
        services.AddScoped<TService>();
        services.AddScoped<TInterface>(static provider =>
        {
            var proxyGenerator = new ProxyGenerator();
            var service = provider.GetRequiredService<TService>;
            var interceptor = provider.GetRequiredService<ValidationInterceptor>();
            return proxyGenerator.CreateInterfaceProxyWithTarget<TService>(
                service(),
                interceptor);
        });
        return services;
    }
}
