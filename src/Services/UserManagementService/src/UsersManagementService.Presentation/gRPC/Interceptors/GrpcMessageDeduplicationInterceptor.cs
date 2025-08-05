using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Interceptors;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.Presentation.gRPC.Models;
using static UsersManagementService.Presentation.Constants.GrpcExceptionsMessages;

namespace UsersManagementService.Presentation.gRPC.Interceptors;

public class GrpcMessageDeduplicationInterceptor(IMessageDeduplicationService deduplicationService) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var messageId = GetMessageId(request);

        if (string.IsNullOrWhiteSpace(messageId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, MessageIdIsRequired));
        }

        if (await deduplicationService.IsMessageProcessedAsync(messageId, context.CancellationToken))
        {
            var result = await deduplicationService.GetResultAsync<TResponse>(messageId, context.CancellationToken);
            if (result is RpcExceptionInfo ex)
            {
                throw new RpcException(new Status((StatusCode)ex.StatusCode, ex.Details));
            }
            else if (result is not null)
            {
                return result;
            }
            else
            {
                return (new Empty() as TResponse)!;
            }
        }

        try
        {
            var response = await continuation(request, context);

            if (response is Empty)
            {
                await deduplicationService.MarkMessageAsProcessedAsync<TResponse>(messageId, null, context.CancellationToken);
            }
            else
            {
                await deduplicationService.MarkMessageAsProcessedAsync<TResponse>(messageId, response, context.CancellationToken);
            }

            return response;
        }
        catch (RpcException ex)
        {
            var exceptionInfo = new RpcExceptionInfo((int)StatusCode.Internal, ex.Status.Detail);
            await deduplicationService.MarkMessageAsProcessedAsync<RpcExceptionInfo>(messageId, exceptionInfo, context.CancellationToken);
            throw;
        }
    }

    private static string? GetMessageId<TRequest>(TRequest request)
    {
        return request?.GetType().GetProperty("MessageId")?.GetValue(request) as string;
    }
}
