using FastEndpoints;

namespace RocketOps.Monitoring.Application.Events;

public class ServiceConfigurationChangedEvent : EventBase
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
