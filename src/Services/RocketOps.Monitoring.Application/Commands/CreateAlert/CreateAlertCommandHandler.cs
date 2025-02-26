using Microsoft.Extensions.Logging;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Domain.Models;
using RocketOps.Monitoring.Domain.Repositories;

namespace RocketOps.Monitoring.Application.Commands.CreateAlert;

/// <summary>
/// Handler for creating new alerts
/// </summary>
public class CreateAlertCommandHandler : ICqrsCommandHandler<CreateAlertCommand>
{
    private readonly IAlertRepository _alertRepository;
    private readonly ILogger<CreateAlertCommandHandler> _logger;

    public CreateAlertCommandHandler(
        IAlertRepository alertRepository,
        ILogger<CreateAlertCommandHandler> logger)
    {
        _alertRepository = alertRepository;
        _logger = logger;
    }

    public async Task HandleAsync(
        CreateAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new alert: {Name}", command.Name);

        // Create a new alert
        var alert = new Alert
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            Severity = command.Severity,
            ServiceName = command.ServiceName,
            Status = "New",
            CreatedAt = DateTime.UtcNow
        };

        // Save alert to repository
        await _alertRepository.AddAsync(alert, cancellationToken);

        // You could raise a domain event here if needed
        // var alertCreatedEvent = new AlertCreatedEvent(alert.Id, alert.Name, alert.Severity);
        // await _eventPublisher.PublishAsync(alertCreatedEvent, cancellationToken);
    }
}