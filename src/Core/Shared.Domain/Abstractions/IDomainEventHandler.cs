namespace Shared.Domain.Abstractions;

/// <summary>
/// Base handler for domain events
/// </summary>
/// <typeparam name="TDomainEvent">Type of domain event</typeparam>
public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Handle a specific domain event
    /// </summary>
    /// <param name="domainEvent">Event to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
