using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Core.Events;

public class MediatorDomainEventPublisher(IMediator mediator) : IDomainEventPublisher
{
    public async Task Publish<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        await mediator.Publish(domainEvent, cancellationToken);
    }
}
