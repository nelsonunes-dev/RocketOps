using MediatR;

namespace RocketOps.Monitoring.Application.Queries.GetServiceHealth;

public class GetServiceHealthQuery : IRequest<ServiceHealthDto>
{
    public Guid ServiceId { get; set; }
}
