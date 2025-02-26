namespace RocketOps.Monitoring.Application.Queries.GetServiceMetrics;

public class ServiceMetricsDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public List<MetricPointDto> Availability { get; set; } = new();
    public List<MetricPointDto> ResponseTime { get; set; } = new();
    public Dictionary<string, List<MetricPointDto>> CustomMetrics { get; set; } = new();
}

public class MetricPointDto
{
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}
