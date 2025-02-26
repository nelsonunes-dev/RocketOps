using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Application.Queries.GetAlerts;

namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Get;

public class GetAlertsEndpoint : Endpoint<GetAlertsRequest, GetAlertsResponse>
{
    private readonly ICqrsQueryHandler<GetAlertsQuery, IEnumerable<AlertDto>> _queryHandler;

    public GetAlertsEndpoint(ICqrsQueryHandler<GetAlertsQuery, IEnumerable<AlertDto>> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public override void Configure()
    {
        Get("/api/alerts");
        AllowAnonymous(); // Replace with proper auth configuration
        Description(b => b
            .WithName("GetAlerts")
            .Produces<GetAlertsResponse>(200, "application/json")
            .WithTags("Alerts"));
    }

    public override async Task HandleAsync(GetAlertsRequest req, CancellationToken ct)
    {
        var query = new GetAlertsQuery { SeverityFilter = req.Severity };
        var result = await _queryHandler.HandleAsync(query, ct);

        var response = new GetAlertsResponse
        {
            Alerts = result.ToList()
        };

        await SendOkAsync(response, ct);
    }
}
