using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Core.Events;

public sealed class PlayersBalancesChangedDomainEvent(
    List<PlayerBalanceChange> changes) : INotification
{
    public IReadOnlyList<PlayerBalanceChange> Changes { get; } = changes;
}
