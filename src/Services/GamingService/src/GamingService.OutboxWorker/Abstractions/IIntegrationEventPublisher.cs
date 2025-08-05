namespace GamingService.OutboxWorker.Abstractions;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) 
        where T : class;
}