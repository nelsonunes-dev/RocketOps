using Shared.Domain.Events.Base;

namespace Shared.Domain.Events;

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
