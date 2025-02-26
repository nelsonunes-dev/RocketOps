using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RocketOps.Core.Infrastructure.Endpoints;

public class HealthCheckEndpoint : EndpointWithoutRequest
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckEndpoint(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    public override void Configure()
    {
        Get("/health");
        AllowAnonymous();
        Description(b => b
            .WithName("HealthCheck")
            .Produces(200)
            .ProducesProblem(503)
            .WithTags("Health"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var report = await _healthCheckService.CheckHealthAsync(ct);

        var response = new
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration,
            Checks = report.Entries.Select(e => new
            {
                Component = e.Key,
                Status = e.Value.Status.ToString(),
                e.Value.Description,
                e.Value.Duration
            })
        };

        if (report.Status == HealthStatus.Healthy)
        {
            await SendOkAsync(response, ct);
        }
        else
        {
            await SendAsync(response, statusCode: 503, ct);
        }
    }
}
