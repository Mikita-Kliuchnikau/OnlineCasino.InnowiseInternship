namespace UsersManagementService.DAL.Interfaces.Services;

public interface IRedisMessageDeduplicationService
{
    Task<bool> IsMessageProcessedAsync(string messageId, CancellationToken cancellationToken = default);
    Task MarkMessageAsProcessedAsync(
        string messageId,
        string response,
        TimeSpan expiry, 
        CancellationToken cancellationToken = default);

    Task<string?> GetResultAsync(string messageId, CancellationToken cancellationToken = default);
}
