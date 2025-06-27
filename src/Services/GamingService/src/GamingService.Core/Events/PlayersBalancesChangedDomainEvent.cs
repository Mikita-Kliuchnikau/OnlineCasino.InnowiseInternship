using GamingService.Core.Contracts;
using GamingService.Core.Primitives;
using MediatR;

namespace GamingService.Core.Events;

public record PlayersBalancesChangedDomainEvent(Id SessionId) : INotification;
