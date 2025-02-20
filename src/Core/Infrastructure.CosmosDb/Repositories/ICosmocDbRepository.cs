using Shared.Data.Abstractions;
using Shared.Domain.Entities.Base;

namespace Infrastructure.CosmosDb.Repositories;

/// <summary>
/// Base interface for CosmosDB repositories
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository</typeparam>
public interface ICosmosDbRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Upsert an entity (insert or update)
    /// </summary>
    /// <param name="entity">Entity to upsert</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The upserted entity</returns>
    Task<TEntity> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if an entity exists by its ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if entity exists, otherwise false</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Patch (partially update) an entity
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="patchDocument">Patch operations</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The patched entity</returns>
    Task<TEntity> PatchAsync(Guid id, object patchDocument, CancellationToken cancellationToken = default);
}
