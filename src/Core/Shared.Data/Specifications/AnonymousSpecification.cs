using Shared.Data.Specifications.Base;
using Shared.Domain.Entities.Base;

namespace Shared.Data.Specifications;

/// <summary>
/// Anonymous implementation of specification for quick creation
/// </summary>
internal class AnonymousSpecification<T> : BaseSpecification<T> where T : Entity
{
    public AnonymousSpecification() : base() { }

    public AnonymousSpecification(Expression<Func<T, bool>> criteria) : base(criteria) { }
}
