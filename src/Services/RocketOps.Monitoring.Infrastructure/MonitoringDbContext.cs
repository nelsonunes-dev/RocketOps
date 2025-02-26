using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace RocketOps.Monitoring.Infrastructure;

public class MonitoringDbContext
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _alertsContainer;

    public MonitoringDbContext(CosmosClient cosmosClient, IConfiguration configuration)
    {
        _cosmosClient = cosmosClient;
        var databaseName = configuration["CosmosDb:DatabaseName"];
        _alertsContainer = _cosmosClient.GetContainer(databaseName, "Alerts");
    }

    public Container Alerts => _alertsContainer;
}
