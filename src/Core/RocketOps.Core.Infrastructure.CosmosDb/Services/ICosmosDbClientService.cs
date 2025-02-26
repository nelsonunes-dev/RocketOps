using Microsoft.Azure.Cosmos;

namespace RocketOps.Core.Infrastructure.CosmosDb.Services;

public interface ICosmosDbClientService
{
    Container GetContainer<T>();
    Container GetContainer(string containerName);
    Task<bool> HealthCheckAsync(CancellationToken cancellationToken = default);
}
