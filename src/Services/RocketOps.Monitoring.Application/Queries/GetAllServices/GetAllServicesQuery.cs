using MediatR;

namespace RocketOps.Monitoring.Application.Queries.GetAllServices;

public class GetAllServicesQuery : IRequest<List<ServiceSummaryDto>>
{
    public bool IncludeInactive { get; set; } = false;
}
