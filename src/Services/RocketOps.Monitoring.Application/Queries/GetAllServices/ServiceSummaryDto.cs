using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Application.Queries.GetAllServices;

public class ServiceSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BaseUrl { get; set; }
    public bool IsActive { get; set; }
    public HealthStatus CurrentHealth { get; set; }
    public DateTime LastUpdated { get; set; }
    public int CheckCount { get; set; }
}
