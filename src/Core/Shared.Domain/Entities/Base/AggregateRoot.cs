using Shared.Domain.Abstractions;

namespace Shared.Domain.Entities.Base;

/// <summary>
/// Represents an aggregate root in Domain-Driven Design
/// </summary>
public abstract class AggregateRoot : Entity
{
    /// <summary>
    /// Tracks domain events for the aggregate
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Gets the domain events
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event
    /// </summary>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clears all domain events
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
