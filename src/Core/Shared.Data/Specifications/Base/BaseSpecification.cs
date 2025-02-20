using Shared.Data.Abstractions;
using Shared.Domain.Entities.Base;

namespace Shared.Data.Specifications.Base;

/// <summary>
/// Base implementation of specification pattern
/// </summary>
/// <typeparam name="T">The type of entity to specify</typeparam>
public abstract class BaseSpecification<T> : ISpecification<T> where T : Entity
{
    /// <inheritdoc/>
    public Expression<Func<T, bool>> Criteria { get; }

    /// <inheritdoc/>
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    /// <inheritdoc/>
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    /// <inheritdoc/>
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    /// <inheritdoc/>
    public int Take { get; private set; }

    /// <inheritdoc/>
    public int Skip { get; private set; }

    /// <inheritdoc/>
    public bool IsPagingEnabled { get; private set; }

    /// <summary>
    /// Creates a specification with optional criteria
    /// </summary>
    /// <param name="criteria">Optional filtering criteria</param>
    protected BaseSpecification(Expression<Func<T, bool>>? criteria = null)
    {
        Criteria = criteria ?? (x => true);
    }

    /// <summary>
    /// Adds an include expression for eager loading
    /// </summary>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    /// <summary>
    /// Sets ascending order
    /// </summary>
    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        OrderByDescending = null;
    }

    /// <summary>
    /// Sets descending order
    /// </summary>
    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDescending = orderByExpression;
        OrderBy = null;
    }

    /// <summary>
    /// Applies pagination
    /// </summary>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
