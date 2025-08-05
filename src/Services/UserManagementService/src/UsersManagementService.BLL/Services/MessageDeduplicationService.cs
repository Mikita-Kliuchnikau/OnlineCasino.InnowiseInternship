using Microsoft.Extensions.Options;
using System.Text.Json;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.DAL.Interfaces.Services;
using UsersManagementService.DAL.Options;

namespace UsersManagementService.BLL.Services;

public class MessageDeduplicationService(IRedisMessageDeduplicationService deduplicationService, IOptions<RedisOptions> options) 
    : IMessageDeduplicationService
{
    public async Task<bool> IsMessageProcessedAsync(string messageId, CancellationToken cancellationToken = default)
    {
        return await deduplicationService.IsMessageProcessedAsync(messageId, cancellationToken);
    }

    public async Task MarkMessageAsProcessedAsync<TResponse>(string messageId, TResponse? response, CancellationToken cancellationToken = default)
    {
        var expiry = options.Value.MessageExpiry;

        var serializedResponse =  response is null
            ? string.Empty
            : JsonSerializer.Serialize(response);
        await deduplicationService.MarkMessageAsProcessedAsync(messageId, serializedResponse, expiry, cancellationToken);
    }

    public async Task<TResponse?> GetResultAsync<TResponse>(string messageId, CancellationToken cancellationToken = default)
    {
        var result = await deduplicationService.GetResultAsync(messageId, cancellationToken);
        if (string.IsNullOrEmpty(result))
        {
            return default;
        }
        return JsonSerializer.Deserialize<TResponse>(result);
    }
}
