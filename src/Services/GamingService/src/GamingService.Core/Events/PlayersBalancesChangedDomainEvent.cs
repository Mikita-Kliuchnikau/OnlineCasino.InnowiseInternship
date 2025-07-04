using MediatR;

namespace GamingService.Core.Events;

public record PlayersBalancesChangedDomainEvent(string SessionId) : INotification;
