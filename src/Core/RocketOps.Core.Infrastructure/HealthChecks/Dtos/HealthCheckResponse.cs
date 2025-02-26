using RocketOps.Core.Domain.Models;

namespace RocketOps.Core.Infrastructure.HealthChecks.Dtos;

public class HealthCheckResponse
{
    public string Status { get; set; } = default!;
    public TimeSpan TotalDuration { get; set; }
    public List<HealthCheck> Results { get; set; } = new();
}
