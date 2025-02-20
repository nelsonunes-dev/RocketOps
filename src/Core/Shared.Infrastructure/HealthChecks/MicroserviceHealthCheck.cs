using Ocelot.Configuration.Repository;

namespace Shared.Infrastructure.HealthChecks;

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
        var httpClient = _httpClientFactory.CreateClient();
        var results = new Dictionary<string, object>();
        var allHealthy = true;

        // Read services from configuration
        var serviceSection = _configuration.GetSection("Services");
        var services = serviceSection.GetChildren()
            .ToDictionary(x => x.Key, x => x.Value);

        // If no services defined in config, fall back to defaults
        if (services.Count == 0)
        {
            services = new Dictionary<string, string>
            {
                { "alerts", "http://alerts-service" },
                { "monitoring", "http://monitoring-service" },
                { "reporting", "http://reporting-service" }
            };
        }

        foreach (var service in services)
        {
            var healthUrl = $"{service.Value}/health";

            try
            {
                var response = await httpClient.GetAsync(healthUrl, cancellationToken);
                results[service.Key] = response.IsSuccessStatusCode ? "Healthy" : "Unhealthy";

                if (!response.IsSuccessStatusCode)
                {
                    allHealthy = false;
                }
            }
            catch
            {
                results[service.Key] = "Unreachable";
                allHealthy = false;
            }
        }

        return allHealthy
            ? HealthCheckResult.Healthy("All microservices are healthy", results)
            : HealthCheckResult.Unhealthy("One or more microservices are unhealthy", null, results);
    }
}
