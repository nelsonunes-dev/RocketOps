using Microsoft.Extensions.Logging;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Domain.Repositories;

namespace RocketOps.Monitoring.Application.Queries.GetAlerts;

public class GetAlertsQueryHandler : ICqrsQueryHandler<GetAlertsQuery, IEnumerable<AlertDto>>
{
    private readonly IAlertRepository _alertRepository;
    private readonly ILogger<GetAlertsQueryHandler> _logger;

    public GetAlertsQueryHandler(
        IAlertRepository alertRepository,
        ILogger<GetAlertsQueryHandler> logger)
    {
        _alertRepository = alertRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<AlertDto>> HandleAsync(
        GetAlertsQuery query,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting alerts with status filter: {Status}", query.Status ?? "All");

        IEnumerable<Domain.Models.Alert> alerts;

        if (!string.IsNullOrEmpty(query.Status))
        {
            alerts = await _alertRepository.GetByStatusAsync(query.Status, cancellationToken);
        }
        else
        {
            alerts = await _alertRepository.GetAllAsync(cancellationToken);
        }

        return alerts.Select(a => new AlertDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            Severity = a.Severity,
            Status = a.Status,
            CreatedAt = a.CreatedAt
        });
    }
}
