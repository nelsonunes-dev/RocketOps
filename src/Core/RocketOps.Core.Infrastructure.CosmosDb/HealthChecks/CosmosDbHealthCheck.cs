using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace RocketOps.Core.Infrastructure.CosmosDb.HealthChecks;

public class CosmosDbHealthCheck : IHealthCheck
{
    private readonly CosmosClient _cosmosClient;
    private readonly ILogger<CosmosDbHealthCheck> _logger;

    public CosmosDbHealthCheck(CosmosClient cosmosClient, ILogger<CosmosDbHealthCheck> logger)
    {
        _cosmosClient = cosmosClient;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // This will throw if the client can't connect
            var databaseResponse = await _cosmosClient.ReadAccountAsync();

            return HealthCheckResult.Healthy("CosmosDB connection is healthy");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CosmosDB health check failed");
            return HealthCheckResult.Unhealthy("CosmosDB connection failed", ex);
        }
    }
}
