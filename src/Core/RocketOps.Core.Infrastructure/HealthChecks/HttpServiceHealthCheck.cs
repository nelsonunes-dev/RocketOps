using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RocketOps.Core.Infrastructure.HealthChecks;

public class HttpServiceHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _serviceUri;
    private readonly string _serviceName;

    public HttpServiceHealthCheck(IHttpClientFactory httpClientFactory, string serviceUri, string serviceName)
    {
        _httpClientFactory = httpClientFactory;
        _serviceUri = serviceUri;
        _serviceName = serviceName;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_serviceUri}/health", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"{_serviceName} is healthy");
            }

            return HealthCheckResult.Degraded($"{_serviceName} returned status code {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"{_serviceName} health check failed", ex);
        }
    }
}
