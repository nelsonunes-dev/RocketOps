using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.CosmosDb.Services;
using System.Net;


namespace RocketOps.Core.Infrastructure.CosmosDb.Repositories;

public abstract class BaseCosmosRepository<T> : ICosmosDbRepository<T> where T : class
{
    protected readonly Container _container;
    protected readonly ILogger _logger;
    protected BaseCosmosRepository(ICosmosDbClientService cosmosService, ILogger logger)
    {
        _container = cosmosService.GetContainer<T>();
        _logger = logger;
    }

    public virtual async Task<T?> GetByIdAsync(
        string id,
        string partitionKey,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(
                id,
                new PartitionKey(partitionKey),
                cancellationToken: ct);

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Item {Id} not found in container {Container}", id, _container.Id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item {Id} from container {Container}", id, _container.Id);
            throw;
        }
    }

    public virtual async Task<IEnumerable<T>> QueryItemsAsync(QueryDefinition queryDefinition, CancellationToken ct = default)
    {
        try
        {
            var query = _container.GetItemQueryIterator<T>(queryDefinition);
            var results = new List<T>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync(ct);
                results.AddRange(response);
            }

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error querying items from container {Container}", _container.Id);
            throw;
        }
    }

    public virtual async Task<T> AddAsync(T entity, string partitionKey, CancellationToken ct = default)
    {
        try
        {
            var response = await _container.CreateItemAsync(
                entity,
                new PartitionKey(partitionKey),
                cancellationToken: ct);

            return response.Resource;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding item to container {Container}", _container.Id);
            throw;
        }
    }

    public virtual async Task UpdateAsync(string id, T entity, string partitionKey, CancellationToken ct = default)
    {
        try
        {
            await _container.ReplaceItemAsync(
                entity,
                id,
                new PartitionKey(partitionKey),
                cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item {Id} in container {Container}", id, _container.Id);
            throw;
        }
    }

    public virtual async Task DeleteAsync(string id, string partitionKey, CancellationToken ct = default)
    {
        try
        {
            await _container.DeleteItemAsync<T>(
                id,
                new PartitionKey(partitionKey),
                cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item {Id} from container {Container}", id, _container.Id);
            throw;
        }
    }
}
