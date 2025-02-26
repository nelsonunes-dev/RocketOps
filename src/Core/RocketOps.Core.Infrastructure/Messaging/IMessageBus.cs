using RocketOps.Core.Domain.Events;

namespace RocketOps.Core.Infrastructure.Messaging;

public interface IMessageBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
}
