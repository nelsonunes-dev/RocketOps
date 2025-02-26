using RocketOps.Core.Domain.Events;

namespace RocketOps.Monitoring.Domain.Events;

public class AlertTriggeredEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public Guid AggregateId { get; }

    public Guid AlertId { get; }
    public string ServiceName { get; }
    public string Severity { get; }

    public AlertTriggeredEvent(Guid alertId, Guid aggregateId, string serviceName, string severity)
    {
        AlertId = alertId;
        AggregateId = aggregateId;
        ServiceName = serviceName;
        Severity = severity;
    }
}
