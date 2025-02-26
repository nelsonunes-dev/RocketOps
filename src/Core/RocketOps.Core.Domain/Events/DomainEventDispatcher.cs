using MediatR;

namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Default implementation of domain event dispatcher
/// </summary>
public class DomainEventDispatcher // : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <inheritdoc/>
    public async Task DispatchAsync(IEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(domainEvent, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DispatchAsync(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
