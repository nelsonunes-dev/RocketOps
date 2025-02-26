using RocketOps.Core.Domain.Events;

namespace RocketOps.Monitoring.Domain.Events;

public class AlertCreatedEvent : IDomainEvent
{
    public Guid Id { get; }
    public DateTime Timestamp { get; }
    public Guid AggregateId { get; }
    public string AlertName { get; }
    public string AlertDescription { get; }
    public string Severity { get; }

    public AlertCreatedEvent(Guid alertId, string alertName, string alertDescription, string severity)
    {
        Id = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        AggregateId = alertId;
        AlertName = alertName;
        AlertDescription = alertDescription;
        Severity = severity;
    }
}
