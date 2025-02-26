using MediatR;

namespace RocketOps.Monitoring.Application.Queries.GetServiceMetrics;

public class GetServiceMetricsQuery : IRequest<ServiceMetricsDto>
{
    public Guid ServiceId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
