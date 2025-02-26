using Microsoft.Azure.Cosmos;

namespace RocketOps.Core.Infrastructure.CosmosDb.Repositories;

/// <summary>
/// Base interface for CosmosDB repositories
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository</typeparam>
public interface ICosmosDbRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id, string partitionKey, CancellationToken ct = default);
    Task<IEnumerable<T>> QueryItemsAsync(QueryDefinition queryDefinition, CancellationToken ct = default);
    Task<T> AddAsync(T entity, string partitionKey, CancellationToken ct = default);
    Task UpdateAsync(string id, T entity, string partitionKey, CancellationToken ct = default);
    Task DeleteAsync(string id, string partitionKey, CancellationToken ct = default);
}
