using MediatR;
using RocketOps.Monitoring.Domain.Enums;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Queries.GetServiceHealth;

public class GetServiceHealthQueryHandler : IRequestHandler<GetServiceHealthQuery, ServiceHealthDto>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IHealthCheckResultRepository _healthCheckResultRepository;

    public GetServiceHealthQueryHandler(
        IServiceRepository serviceRepository,
        IHealthCheckResultRepository healthCheckResultRepository)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _healthCheckResultRepository = healthCheckResultRepository ?? throw new ArgumentNullException(nameof(healthCheckResultRepository));
    }

    public async Task<ServiceHealthDto> Handle(GetServiceHealthQuery request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service == null)
        {
            return null;
        }

        var recentResults = await _healthCheckResultRepository.GetRecentByServiceIdAsync(request.ServiceId);

        var healthDto = new ServiceHealthDto
        {
            ServiceId = service.Id,
            ServiceName = service.Name,
            Status = service.CurrentHealth,
            LastUpdated = service.LastUpdated,
            CheckResults = new List<ServiceCheckHealthDto>()
        };

        // Group results by check ID and take the most recent for each
        var groupedResults = recentResults.GroupBy(r => r.Status).ToDictionary(g => g.Key, g => g.OrderByDescending(r => r.Name).First());

        foreach (var check in service.ServiceChecks)
        {
            if (groupedResults.TryGetValue(check.Id.ToString(), out var result))
            {
                healthDto.CheckResults.Add(new ServiceCheckHealthDto
                {
                    CheckId = check.Id,
                    CheckName = check.Name,
                    Status = Enum.Parse<HealthStatus>(result.Status, ignoreCase: true),
                    LastChecked = result.Timestamp,
                    LastResponseTimeMs = result.ResponseTimeMs,
                    LastMessage = result.Message
                });
            }
            else
            {
                healthDto.CheckResults.Add(new ServiceCheckHealthDto
                {
                    CheckId = check.Id,
                    CheckName = check.Name,
                    Status = HealthStatus.Unknown,
                    LastChecked = check.LastUpdated,
                    LastResponseTimeMs = 0,
                    LastMessage = "No health check results available"
                });
            }
        }

        return healthDto;
    }
}
