namespace Shared.Domain.Abstractions;

/// <summary>
/// Marker interface for domain events
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Unique identifier for the event
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Timestamp of the event
    /// </summary>
    DateTime Timestamp { get; }
}
