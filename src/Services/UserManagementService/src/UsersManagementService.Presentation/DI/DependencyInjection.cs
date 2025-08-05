using UsersManagementService.BLL.DI;
using UsersManagementService.Presentation.gRPC.Interceptors;
using UsersManagementService.Presentation.Models;
using UsersManagementService.Presentation.Options;


namespace UsersManagementService.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBLL(configuration);
        services.AddDtoMappingConfig();

        services.ConfigureOptions<Auth0OptionsSetup>();
        services.ConfigureOptions<GrpcOptionsSetup>();

        services.AddGrpc(opt =>
        {
            opt.Interceptors.Add<GrpcLoggingInterceptor>();
            opt.Interceptors.Add<GrpcMessageDeduplicationInterceptor>();
            opt.Interceptors.Add<GrpcAuthenticationInterceptor>();
        });

        return services;
    }
}
