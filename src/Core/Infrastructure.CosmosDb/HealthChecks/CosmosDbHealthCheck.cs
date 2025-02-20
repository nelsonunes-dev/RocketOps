namespace Infrastructure.CosmosDb.HealthChecks;

public class CosmosDbHealthCheck : IHealthCheck
{
    private readonly CosmosClient _cosmosClient;

    public CosmosDbHealthCheck(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Simple account availability check
            await _cosmosClient.ReadAccountAsync();
            return HealthCheckResult.Healthy("CosmosDB connection is healthy");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("CosmosDB connection is unhealthy", ex);
        }
    }
}
