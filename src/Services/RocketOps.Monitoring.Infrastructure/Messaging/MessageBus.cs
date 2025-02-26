using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Domain.Events;
using RocketOps.Core.Infrastructure.Messaging;
using System.Collections.Concurrent;

namespace RocketOps.Monitoring.Infrastructure.Messaging;

public class MessageBus : IMessageBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MessageBus> _logger;
    private static readonly ConcurrentDictionary<Type, List<Type>> _eventHandlersMapping = new();

    public MessageBus(IServiceProvider serviceProvider, ILogger<MessageBus> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        var eventType = @event.GetType();

        if (_eventHandlersMapping.TryGetValue(eventType, out var handlerTypes))
        {
            using var scope = _serviceProvider.CreateScope();

            foreach (var handlerType in handlerTypes)
            {
                var handler = scope.ServiceProvider.GetService(handlerType);

                if (handler == null)
                {
                    _logger.LogWarning("No handler registered for {EventType}", eventType.Name);
                    continue;
                }

                var handleMethod = handlerType.GetMethod("HandleAsync");

                if (handleMethod != null)
                {
                    try
                    {
                        await (Task)handleMethod.Invoke(handler, [@event, cancellationToken]);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error handling event {EventType}", eventType.Name);
                    }
                }
            }
        }
    }

    public static void Subscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IMessageHandler<TEvent>
    {
        var eventType = typeof(TEvent);
        var handlerType = typeof(THandler);

        if (!_eventHandlersMapping.ContainsKey(eventType))
        {
            _eventHandlersMapping[eventType] = new List<Type>();
        }

        if (!_eventHandlersMapping[eventType].Contains(handlerType))
        {
            _eventHandlersMapping[eventType].Add(handlerType);
        }
    }
}
