using MediatR;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Queries.GetServiceMetrics;

public class GetServiceMetricsQueryHandler : IRequestHandler<GetServiceMetricsQuery, ServiceMetricsDto>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMetricsRepository _metricsRepository;

    public GetServiceMetricsQueryHandler(IServiceRepository serviceRepository, IMetricsRepository metricsRepository)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _metricsRepository = metricsRepository ?? throw new ArgumentNullException(nameof(metricsRepository));
    }

    public async Task<ServiceMetricsDto> Handle(GetServiceMetricsQuery request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service == null)
        {
            return null;
        }

        var from = request.From ?? DateTime.UtcNow.AddDays(-1);
        var to = request.To ?? DateTime.UtcNow;

        var metrics = await _metricsRepository.GetByServiceIdAsync(request.ServiceId, from, to);

        var metricsDto = new ServiceMetricsDto
        {
            ServiceId = service.Id,
            ServiceName = service.Name
        };

        // Calculate availability (success rate)
        var timeGroups = metrics
            .GroupBy(m => m.Timestamp.ToString("yyyy-MM-dd HH:mm"))
            .OrderBy(g => g.Key);

        foreach (var group in timeGroups)
        {
            var totalChecks = group.Count();
            var successfulChecks = group.Count(m => m.IsSuccess);
            var availabilityPercentage = totalChecks > 0
                ? (double)successfulChecks / totalChecks * 100
                : 0;

            var timestamp = group.First().Timestamp;

            metricsDto.Availability.Add(new MetricPointDto
            {
                Timestamp = timestamp,
                Value = availabilityPercentage
            });

            // Average response time
            var avgResponseTime = group.Average(m => m.ResponseTimeMs);
            metricsDto.ResponseTime.Add(new MetricPointDto
            {
                Timestamp = timestamp,
                Value = avgResponseTime
            });
        }

        // Process custom metrics
        var allCustomMetricKeys = metrics
            .SelectMany(m => m.CustomMetrics.Keys)
            .Distinct()
            .ToList();

        foreach (var key in allCustomMetricKeys)
        {
            metricsDto.CustomMetrics[key] = new List<MetricPointDto>();

            foreach (var group in timeGroups)
            {
                var metricsWithKey = group
                    .Where(m => m.CustomMetrics.ContainsKey(key))
                    .ToList();

                if (metricsWithKey.Any())
                {
                    var avgValue = metricsWithKey.Average(m => m.CustomMetrics[key]);
                    metricsDto.CustomMetrics[key].Add(new MetricPointDto
                    {
                        Timestamp = group.First().Timestamp,
                        Value = avgValue
                    });
                }
            }
        }

        return metricsDto;
    }
}
