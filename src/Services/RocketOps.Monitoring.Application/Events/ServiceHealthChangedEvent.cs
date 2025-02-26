using FastEndpoints;
using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Application.Events;

public class ServiceHealthChangedEvent : EventBase
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public HealthStatus PreviousStatus { get; set; }
    public HealthStatus CurrentStatus { get; set; }
    public string Message { get; set; } = default!;
}
