using RocketOps.Core.Data.Specifications;
using RocketOps.Core.Data.Specifications.Base;
using RocketOps.Core.Domain.Entities.Base;

namespace RocketOps.Core.Data.Helpers;

/// <summary>
/// Query helper methods for building specifications
/// </summary>
public static class SpecificationHelpers
{
    /// <summary>
    /// Creates a specification for filtering entities by ID
    /// </summary>
    public static BaseSpecification<T> ById<T>(Guid id) where T : Entity
    {
        return new AnonymousSpecification<T>(x => x.Id == id);
    }

    /// <summary>
    /// Creates a specification for filtering entities created after a specific date
    /// </summary>
    public static BaseSpecification<T> CreatedAfter<T>(DateTime date) where T : Entity
    {
        return new AnonymousSpecification<T>(x => x.CreatedAt > date);
    }

    /// <summary>
    /// Creates a specification for paginated results
    /// </summary>
    public static BaseSpecification<T> Paginate<T>(int page, int pageSize) where T : Entity
    {
        var spec = new PaginatedSpecification<T>(page, pageSize);
        return spec;
    }
}
