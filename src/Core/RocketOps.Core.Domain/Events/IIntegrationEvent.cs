namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Interface for integration events that are published across services
/// </summary>
public interface IIntegrationEvent : IEvent
{
    string EventType { get; }
}
