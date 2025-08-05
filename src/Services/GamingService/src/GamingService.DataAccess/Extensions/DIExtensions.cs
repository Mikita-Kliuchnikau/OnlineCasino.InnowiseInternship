using GamingService.DataAccess.Interceptors;
using GamingService.DataAccess.Options;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static UsersManagementService.Grpc.UsersService;

namespace GamingService.DataAccess.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddGrpcUsersServiceClient(this IServiceCollection services)
    {
        services.AddGrpcClient<UsersServiceClient>((provider, opt) =>
        {
            var grpcOptions = provider.GetRequiredService<IOptions<GrpcOptions>>().Value;
            opt.Address = new Uri(grpcOptions.ServerUrl);
            opt.ChannelOptionsActions.Add(channelOptions =>
            {
                channelOptions.ServiceConfig = new ServiceConfig
                {
                    MethodConfigs =
                    {
                        new MethodConfig
                        {
                            Names = { MethodName.Default },
                            RetryPolicy = new RetryPolicy
                            {
                                MaxAttempts = grpcOptions.MaxAttempts,
                                InitialBackoff = grpcOptions.InitialBackoff,
                                MaxBackoff = grpcOptions.MaxBackoff,
                                BackoffMultiplier = grpcOptions.BackoffMultiplier,
                                RetryableStatusCodes = { StatusCode.Unavailable }
                            }
                        }
                    }
                };
            });
        })
            .AddInterceptor<GrpcClientApiKeyInterceptor>();

        return services;
    }
}
