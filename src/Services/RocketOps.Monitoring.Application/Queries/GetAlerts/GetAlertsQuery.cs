using RocketOps.Core.Application.CQRS;

namespace RocketOps.Monitoring.Application.Queries.GetAlerts;

public class GetAlertsQuery : ICqrsQuery<IEnumerable<AlertDto>>
{
    public string? Status { get; }
    public string? SeverityFilter { get; internal set; }

    public GetAlertsQuery(string? status = null)
    {
        Status = status;
    }
}

//public class GetAlertsQuery : ICqrsQuery<IEnumerable<AlertDto>>
//{
//    public string? Status { get; }
//    public string? SeverityFilter { get; internal set; }

//    public GetAlertsQuery(string? status = null)
//    {
//        Status = status;
//    }
//}
