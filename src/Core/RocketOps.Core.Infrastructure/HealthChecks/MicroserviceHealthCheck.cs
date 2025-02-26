using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RocketOps.Core.Infrastructure.HealthChecks;

public class MicroservicesHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public MicroservicesHealthCheck(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Retrieve microservices to check from configuration
            var microservices = _configuration.GetSection("HealthChecks:Microservices")
                .Get<List<string>>() ?? new List<string>();

            var tasks = microservices.Select(CheckMicroserviceHealth);
            var results = await Task.WhenAll(tasks);

            // If any microservice is down, return Unhealthy
            if (results.Any(r => !r.IsHealthy))
            {
                return HealthCheckResult.Unhealthy(
                    description: "One or more microservices are unhealthy",
                    data: results.ToDictionary(r => r.ServiceName, r => (object)r.IsHealthy)
                );
            }

            return HealthCheckResult.Healthy(
                description: "All microservices are healthy",
                data: results.ToDictionary(r => r.ServiceName, r => (object)r.IsHealthy)
            );
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                description: "Error checking microservices health",
                exception: ex
            );
        }
    }

    private async Task<MicroserviceHealthResult> CheckMicroserviceHealth(string serviceUrl)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            var response = await client.GetAsync($"{serviceUrl}/health/ready");

            return new MicroserviceHealthResult
            {
                ServiceName = serviceUrl,
                IsHealthy = response.IsSuccessStatusCode
            };
        }
        catch
        {
            return new MicroserviceHealthResult
            {
                ServiceName = serviceUrl,
                IsHealthy = false
            };
        }
    }

    private class MicroserviceHealthResult
    {
        public string ServiceName { get; set; } = default!;
        public bool IsHealthy { get; set; }
    }
}
