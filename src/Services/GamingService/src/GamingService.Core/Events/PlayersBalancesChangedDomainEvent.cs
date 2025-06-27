using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Core.Events;

public record PlayersBalancesChangedDomainEvent(IReadOnlyList<PlayerBalanceChange> Changes) : INotification;
