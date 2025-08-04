using GamingService.DataAccess.Options;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;

namespace GamingService.DataAccess.Interceptors;

public class GrpcClientApiKeyInterceptor(IOptions<GrpcOptions> options) : Interceptor
{
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request, 
        ClientInterceptorContext<TRequest, TResponse> context, 
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        AddCallerMetadata(ref context, options);
        return continuation(request, context);
    }

    private static void AddCallerMetadata<TRequest, TResponse>(
        ref ClientInterceptorContext<TRequest, TResponse> context,
        IOptions<GrpcOptions> options)
            where TRequest : class
            where TResponse : class
    {
        var headers = context.Options.Headers;

        if (headers is null)
        {
            headers = [];
            var contextOptions = context.Options.WithHeaders(headers);
            context = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method, 
                context.Host, 
                contextOptions);
        }

        var apiKeyHeaderName = options.Value.ApiKeyHeaderName;

        headers.Add(apiKeyHeaderName, options.Value.ApiKey);
    }
}
