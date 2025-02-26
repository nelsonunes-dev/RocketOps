using RocketOps.Core.Data.Specifications.Base;
using RocketOps.Core.Domain.Entities.Base;
using System.Linq.Expressions;

namespace RocketOps.Core.Data.Specifications;

/// <summary>
/// Anonymous implementation of specification for quick creation
/// </summary>
internal class AnonymousSpecification<T> : BaseSpecification<T> where T : Entity
{
    public AnonymousSpecification() : base() { }

    public AnonymousSpecification(Expression<Func<T, bool>> criteria) : base(criteria) { }
}
