using Grpc.Core;
using Grpc.Core.Interceptors;

namespace UsersManagementService.Presentation.gRPC.Interceptors;

public class GrpcLoggingInterceptor(ILogger<GrpcLoggingInterceptor> logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, 
        ServerCallContext context, 
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        logger.LogInformation(
            "Processing gRPC request {RequestName}, {Method}",
            nameof(request),
            nameof(context.Method));

        try
        {
            var response = await continuation(request, context);

            logger.LogInformation("Complited request {RequestName}, {Method}",
            nameof(request),
            nameof(context.Method));

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing gRPC request {RequestName}, {Method}",
                nameof(request),
                nameof(context.Method));

            throw new RpcException(new Status(StatusCode.Internal,
                    $"Internal error while calling {context.Method}"), ex.Message);
        }
    }
}
