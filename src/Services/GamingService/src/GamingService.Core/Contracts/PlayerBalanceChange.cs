using GamingService.Core.Common;
using GamingService.Core.Primitives;

namespace GamingService.Core.Contracts;

public sealed class PlayerBalanceChange(
    Id playerId,
    Currency currency,
    decimal amount)
{
    public Id PlayerId { get; } = playerId;
    public Currency Currency { get; } = currency;
    public decimal Amount { get; } = amount;
}
