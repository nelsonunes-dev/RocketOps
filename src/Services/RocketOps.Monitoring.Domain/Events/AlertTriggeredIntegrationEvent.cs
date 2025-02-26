using RocketOps.Core.Domain.Events;

namespace RocketOps.Monitoring.Domain.Events;

public class AlertTriggeredIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public DateTime Timestamp { get; }
    public string EventType { get; }
    public Guid AlertId { get; }
    public string ServiceName { get; }
    public string AlertName { get; }
    public string Severity { get; }

    public AlertTriggeredIntegrationEvent(Guid alertId, string serviceName, string alertName, string severity)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        Timestamp = DateTime.UtcNow;
        EventType = nameof(AlertTriggeredIntegrationEvent); // Using the class name as the event type
        AlertId = alertId;
        ServiceName = serviceName;
        AlertName = alertName;
        Severity = severity;
    }
}
