using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;
using UsersManagementService.DAL.Interfaces.Services;
using UsersManagementService.DAL.Options;

namespace UsersManagementService.DAL.Services;

public class RedisMessageDeduplicationService(IConnectionMultiplexer redis, IOptions<RedisOptions> options) 
    : IRedisMessageDeduplicationService
{
    private string KeyPrefix => options.Value.PrefixKey;

    public async Task<bool> IsMessageProcessedAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var db = redis.GetDatabase();
        return await db.KeyExistsAsync($"{KeyPrefix}{messageId}");
    }

    public async Task MarkMessageAsProcessedAsync(
        string messageId, 
        string response, 
        TimeSpan expiry, 
        CancellationToken cancellationToken = default)
    {
        var db = redis.GetDatabase();
        await db.StringSetAsync($"{KeyPrefix}{messageId}", response, expiry);
    }

    public async Task<string?> GetResultAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var db = redis.GetDatabase();
        var value = await db.StringGetAsync($"{KeyPrefix}{messageId}");
        return value.IsNullOrEmpty 
            ? default
            : value;
    }
}
