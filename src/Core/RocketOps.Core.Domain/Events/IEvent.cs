namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Base interface for all events in the system
/// </summary>
public interface IEvent
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}
