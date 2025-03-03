﻿using RocketOps.Core.Domain.Events.Base;

namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Event for tracking entity creation
/// </summary>
/// <typeparam name="TEntity">Type of entity created</typeparam>
public class EntityCreatedEvent<TEntity> : DomainEventBase
{
    /// <summary>
    /// The entity that was created
    /// </summary>
    public TEntity Entity { get; }

    public EntityCreatedEvent(TEntity entity)
    {
        Entity = entity;
    }
}
