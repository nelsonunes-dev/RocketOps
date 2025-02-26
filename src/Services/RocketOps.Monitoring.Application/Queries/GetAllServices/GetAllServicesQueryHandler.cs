using MediatR;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Queries.GetAllServices;

public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, List<ServiceSummaryDto>>
{
    private readonly IServiceRepository _serviceRepository;

    public GetAllServicesQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
    }

    public async Task<List<ServiceSummaryDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await _serviceRepository.GetAllAsync();

        if (!request.IncludeInactive)
        {
            services = services.Where(s => s.IsActive).ToList();
        }

        return services.Select(s => new ServiceSummaryDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            BaseUrl = s.BaseUrl,
            IsActive = s.IsActive,
            CurrentHealth = s.CurrentHealth,
            LastUpdated = s.LastUpdated,
            CheckCount = s.ServiceChecks.Count
        }).ToList();
    }
}
