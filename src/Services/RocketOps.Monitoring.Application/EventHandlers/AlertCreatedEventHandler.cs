using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.Messaging;
using RocketOps.Monitoring.Domain.Events;

namespace RocketOps.Monitoring.Application.EventHandlers;

public class AlertTriggeredEventHandler : IMessageHandler<AlertTriggeredEvent>
{
    private readonly IMessageBus _messageBus;
    private readonly ILogger<AlertTriggeredEventHandler> _logger;

    public AlertTriggeredEventHandler(
        IMessageBus messageBus,
        ILogger<AlertTriggeredEventHandler> logger)
    {
        _messageBus = messageBus;
        _logger = logger;
    }

    public async Task HandleAsync(
        AlertTriggeredEvent @event,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Alert triggered: {AlertId}", @event.AlertId);

        // Create integration event with all required parameters
        var integrationEvent = new AlertTriggeredIntegrationEvent(
            @event.AlertId,             // alertId
            @event.ServiceName,         // serviceName
            DateTime.UtcNow.ToString(),            // triggeredAt
            @event.Severity             // severity
        );

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
