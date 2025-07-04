namespace GamingService.Core.Abstractions;

public interface IDomainEventPublisher
{
    Task Publish<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
        where TEvent : class;
}
