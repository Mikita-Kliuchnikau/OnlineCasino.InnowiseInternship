using MassTransit;
using Microsoft.Extensions.Options;
using UsersManagementService.BLL.DI;
using UsersManagementService.Presentation.EventConsumers;
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
        services.ConfigureOptions<RabbitMqOptionsSetup>();

        services.AddGrpc(opt =>
        {
            opt.Interceptors.Add<GrpcLoggingInterceptor>();
            opt.Interceptors.Add<GrpcMessageDeduplicationInterceptor>();
            opt.Interceptors.Add<GrpcAuthenticationInterceptor>();
        });

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<UserBalanceChangedConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                configurator.Host(options.Host, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

            configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
