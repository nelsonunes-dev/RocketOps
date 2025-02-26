using RocketOps.Core.Domain.Entities.Base;
using System.Linq.Expressions;

namespace RocketOps.Core.Data.Abstractions;

/// <summary>
/// Represents a query specification for filtering and querying entities
/// </summary>
/// <typeparam name="T">The type of entity to specify</typeparam>
public interface ISpecification<T> where T : Entity
{
    /// <summary>
    /// The primary filtering criteria for the specification
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Additional includes to eager load related entities
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Ordering expression for ascending order
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Ordering expression for descending order
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Number of items to take (for pagination)
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Number of items to skip (for pagination)
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Indicates if pagination is enabled
    /// </summary>
    bool IsPagingEnabled { get; }
}
