 using GamingService.Core.Abstractions;
using GamingService.Core.Events;
using GamingService.Core.Models.SessionAggregate;
using MediatR;

namespace GamingService.Application.Events;

public class PlayersBalancesChangedDomainEventHandler(
    IIntegrationEventPublisher publisher,
    ISessionsRepository sessionsRepository)
    : INotificationHandler<PlayersBalancesChangedDomainEvent>
{
    public async Task Handle(PlayersBalancesChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var session = await sessionsRepository.GetByIdAsync(Guid.Parse(notification.SessionId), cancellationToken);
        var winningBets = session.Bets?.Where(bet => bet.Status == BetStatus.Won).ToList();
        if (winningBets != null && winningBets.Count != 0)
        {
            var integrationEventPayload = winningBets
                .Select(bet => new PlayersBalancesChangedEventPayload(bet.PlayerId.ToString(), bet.BetWinnings.Value))
                .ToList();
            var integrationEvent = new PlayersBalancesChangedIntegrationEvent(integrationEventPayload);
            await publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
