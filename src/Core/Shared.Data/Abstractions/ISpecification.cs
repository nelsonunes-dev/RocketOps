using Shared.Domain.Entities.Base;

namespace Shared.Data.Abstractions;

/// <summary>
/// Specification interface for querying
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ISpecification<T> where T : Entity
{
    /// <summary>
    /// Criteria for filtering
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Includes for eager loading
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Order by expressions
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Order by descending
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Paging information
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Paging information
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Pagination enabled
    /// </summary>
    bool IsPagingEnabled { get; }
}
