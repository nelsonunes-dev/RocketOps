using RocketOps.Core.Domain.Events.Base;

namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Event for tracking entity deletion
/// </summary>
/// <typeparam name="TEntity">Type of entity deleted</typeparam>
public class EntityDeletedEvent<TEntity> : DomainEventBase
{
    /// <summary>
    /// The entity that was deleted
    /// </summary>
    public TEntity Entity { get; }

    public EntityDeletedEvent(TEntity entity)
    {
        Entity = entity;
    }
}
