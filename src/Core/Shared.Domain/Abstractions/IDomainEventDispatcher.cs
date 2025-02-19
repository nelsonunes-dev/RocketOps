namespace Shared.Domain.Abstractions;

/// <summary>
/// Domain event dispatcher using MediatR
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatch a domain event
    /// </summary>
    /// <param name="domainEvent">Event to dispatch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dispatch multiple domain events
    /// </summary>
    /// <param name="domainEvents">Events to dispatch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
