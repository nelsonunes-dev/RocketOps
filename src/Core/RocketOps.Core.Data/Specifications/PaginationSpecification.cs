using RocketOps.Core.Data.Specifications.Base;
using RocketOps.Core.Domain.Entities.Base;
using System.Linq.Expressions;

namespace RocketOps.Core.Data.Specifications;

/// <summary>
/// Pagination specification
/// </summary>
internal class PaginatedSpecification<T> : BaseSpecification<T> where T : Entity
{
    public PaginatedSpecification(int page, int pageSize)
    {
        // Adjust for zero-based indexing
        int skip = (page - 1) * pageSize;
        ApplyPaging(skip, pageSize);
    }

    public PaginatedSpecification(Expression<Func<T, bool>> criteria, int page, int pageSize) : base(criteria)
    {
        // Adjust for zero-based indexing
        int skip = (page - 1) * pageSize;
        ApplyPaging(skip, pageSize);
    }
}
