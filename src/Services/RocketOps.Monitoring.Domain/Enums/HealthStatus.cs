namespace RocketOps.Monitoring.Domain.Enums;

/// <summary>
/// Represents the health status of a service
/// </summary>
public enum HealthStatus
{
    Healthy,
    Degraded,
    Unhealthy,
    Unknown
}
