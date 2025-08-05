using GamingService.OutboxWorker.Abstractions;
using MassTransit;

namespace GamingService.OutboxWorker.Events;

public class RabbitMqPlayersBalancesChangedEventPublisher(IPublishEndpoint publishEndpoint) : IIntegrationEventPublisher
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : class
    {
        await publishEndpoint.Publish(integrationEvent, cancellationToken);
    }   
}
