namespace GamingService.Application.Events;

public interface IPlayersBalancesChangedIntegrationEventPublisher
{
    Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event, CancellationToken cancellationToken);
}
