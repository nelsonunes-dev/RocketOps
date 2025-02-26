using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Application.Queries.GetServiceHealth;

public class ServiceHealthDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public HealthStatus Status { get; set; }
    public DateTime LastUpdated { get; set; }
    public List<ServiceCheckHealthDto> CheckResults { get; set; } = new();
}

public class ServiceCheckHealthDto
{
    public Guid CheckId { get; set; }
    public string CheckName { get; set; } = default!;
    public HealthStatus Status { get; set; }
    public DateTime LastChecked { get; set; }
    public double LastResponseTimeMs { get; set; }
    public string LastMessage { get; set; } = default!;
}
