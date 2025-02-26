using RocketOps.Core.Domain.Events.Base;

namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Event for tracking entity updates
/// </summary>
/// <typeparam name="TEntity">Type of entity updated</typeparam>
public class EntityUpdatedEvent<TEntity> : DomainEventBase
{
    /// <summary>
    /// The entity before update
    /// </summary>
    public TEntity OldEntity { get; }

    /// <summary>
    /// The updated entity
    /// </summary>
    public TEntity NewEntity { get; }

    public EntityUpdatedEvent(TEntity oldEntity, TEntity newEntity)
    {
        OldEntity = oldEntity;
        NewEntity = newEntity;
    }
}
