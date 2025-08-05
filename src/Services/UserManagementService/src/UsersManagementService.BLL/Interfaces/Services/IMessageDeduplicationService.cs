namespace UsersManagementService.BLL.Interfaces.Services;

public interface IMessageDeduplicationService
{
    Task<bool> IsMessageProcessedAsync(string messageId, CancellationToken cancellationToken = default);
    
    Task MarkMessageAsProcessedAsync<TResponse>(
        string messageId,
        TResponse? response,
        CancellationToken cancellationToken = default);

    Task<object?> GetResultAsync<TResponse>(string messageId, CancellationToken cancellationToken = default);
}
