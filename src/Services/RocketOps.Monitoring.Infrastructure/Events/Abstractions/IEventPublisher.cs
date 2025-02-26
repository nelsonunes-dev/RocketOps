using RocketOps.Core.Domain.Events;

namespace RocketOps.Monitoring.Infrastructure.Events.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : IEvent;
}
