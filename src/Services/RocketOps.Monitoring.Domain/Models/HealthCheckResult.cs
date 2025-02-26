using RocketOps.Core.Domain.Entities.Base;
using RocketOps.Core.Domain.Models;

namespace RocketOps.Monitoring.Domain.Models;

public class HealthCheckResult : Entity
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public HealthCheck CheckResult { get; set; } = new();

    // Base properties from HealthCheck
    public string Status => CheckResult.Status;
    public string Name => CheckResult.Name;
    public string Exception => CheckResult.Exception;
    public string Duration => CheckResult.Duration;

    // Additional properties needed
    public double ResponseTimeMs { get; set; }
    public string Message { get; set; } = string.Empty;

    // For storing any additional data
    public Dictionary<string, object> AdditionalData { get; set; } = new();
}
