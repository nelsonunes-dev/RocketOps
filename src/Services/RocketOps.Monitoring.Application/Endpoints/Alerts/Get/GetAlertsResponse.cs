using RocketOps.Monitoring.Application.Queries.GetAlerts;

namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Get;

public class GetAlertsResponse
{
    public List<AlertDto> Alerts { get; set; } = new();
}
