namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Interface for domain events that occur within aggregates
/// </summary>
public interface IDomainEvent : IEvent
{
    // Domain events are specific to an aggregate
    Guid AggregateId { get; }
}
