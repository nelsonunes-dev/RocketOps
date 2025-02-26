using RocketOps.Core.Domain.Entities.Base;

namespace RocketOps.Core.Data.Abstractions;

/// <summary>
/// Generic repository interface for basic CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : Entity
{
    /// <summary>
    /// Get an entity by its ID
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Find entities based on a specification
    /// </summary>
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a new entity
    /// </summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an entity
    /// </summary>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count entities matching a specification
    /// </summary>
    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
}
