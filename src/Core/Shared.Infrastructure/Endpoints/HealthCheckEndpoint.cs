using Shared.Domain.Models;
using Shared.Infrastructure.Responses;

namespace Shared.Infrastructure.Endpoints;

public class HealthCheckEndpoint : EndpointWithoutRequest<HealthCheckResponse>
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
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var report = await _healthCheckService.CheckHealthAsync(ct);

        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.ToDictionary(
                e => e.Key,
                e => new
                {
                    Status = e.Value.Status.ToString(),
                    Description = e.Value.Description
                } as object
            )
        };

        await SendAsync(response, cancellation: ct);
    }
}
