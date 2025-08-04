using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using UsersManagementService.Presentation.Options;

namespace UsersManagementService.Presentation.gRPC.Interceptors;

public class GrpcAuthenticationInterceptor(IOptions<GrpcOptions> options) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var apiKey = context.RequestHeaders.GetValue(options.Value.ApiKeyHeaderName);
        var expectedApiKey = options.Value.ApiKey;
        if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, ""));
        }
        return await continuation(request, context);
    }
}
