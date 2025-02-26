using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Application.Dtos;

public class ServiceHealthDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public HealthStatus Status { get; set; }
    public DateTime LastUpdated { get; set; }
    public List<ServiceCheckDto> CheckResults { get; set; } = new();
}
