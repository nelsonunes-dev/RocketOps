using RocketOps.Core.Domain.Events;

namespace RocketOps.Core.Infrastructure.Messaging;

public interface IMessageHandler<TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
