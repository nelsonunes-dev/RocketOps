using MediatR;
using RocketOps.Monitoring.Infrastructure.Events.Abstractions;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Commands.UpdateHealthStatus;

public class UpdateHealthStatusCommandHandler : IRequestHandler<UpdateHealthStatusCommand, bool>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IHealthCheckResultRepository _healthCheckResultRepository;
    private readonly IEventPublisher _eventPublisher;

    public UpdateHealthStatusCommandHandler(
        IServiceRepository serviceRepository,
        IHealthCheckResultRepository healthCheckResultRepository,
        IEventPublisher eventPublisher)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _healthCheckResultRepository = healthCheckResultRepository ?? throw new ArgumentNullException(nameof(healthCheckResultRepository));
        _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
    }

    public async Task<bool> Handle(UpdateHealthStatusCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service == null)
        {
            return false;
        }

        var previousStatus = service.CurrentHealth;
        service.UpdateHealth(request.Status);

        await _serviceRepository.UpdateAsync(service);

        // Create health check result if serviceCheckId is provided
        if (request.ServiceCheckId.HasValue)
        {
            //var healthCheckResult = new HealthCheck(
            //    request.ServiceId,
            //    request.ServiceCheckId.Value,
            //    request.Status,
            //    request.ResponseTimeMs,
            //    request.Message);

            //await _healthCheckResultRepository.AddAsync(healthCheckResult);
        }

        // Publish event if status changed
        if (previousStatus != request.Status)
        {
            //await _eventPublisher.PublishAsync(new ServiceHealthChangedEvent
            //{
            //    ServiceId = service.Id,
            //    ServiceName = service.Name,
            //    PreviousStatus = previousStatus,
            //    CurrentStatus = request.Status,
            //    //Timestamp = DateTime.UtcNow,
            //    Message = request.Message
            //});
        }

        return true;
    }
}
