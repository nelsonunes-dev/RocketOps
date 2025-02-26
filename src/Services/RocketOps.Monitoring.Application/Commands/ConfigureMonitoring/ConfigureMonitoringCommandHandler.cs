using MediatR;
using RocketOps.Monitoring.Domain.Entities;
using RocketOps.Monitoring.Domain.Enums;
using RocketOps.Monitoring.Infrastructure.Events.Abstractions;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Commands.ConfigureMonitoring;

public class ConfigureMonitoringCommandHandler : IRequestHandler<ConfigureMonitoringCommand, bool>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IEventPublisher _eventPublisher;

    public ConfigureMonitoringCommandHandler(IServiceRepository serviceRepository, IEventPublisher eventPublisher)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
    }

    public async Task<bool> Handle(ConfigureMonitoringCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service == null)
        {
            return false;
        }

        // Add new checks
        foreach (var checkDto in request.ChecksToAdd)
        {
            var check = new ServiceCheck(
                checkDto.Name,
                checkDto.Endpoint,
                TimeSpan.FromSeconds(checkDto.IntervalSeconds),
                Enum.Parse<CheckType>(checkDto.CheckType, ignoreCase: true),
                checkDto.TimeoutSeconds,
                checkDto.ExpectedResponse);

            foreach (var header in checkDto.Headers)
            {
                check.AddHeader(header.Key, header.Value);
            }

            service.AddServiceCheck(check);
        }

        // Remove checks
        foreach (var checkId in request.ChecksToRemove)
        {
            service.RemoveServiceCheck(checkId);
        }

        // Update checks
        foreach (var checkDto in request.ChecksToUpdate)
        {
            service.RemoveServiceCheck(checkDto.Id);

            var updatedCheck = new ServiceCheck(
                checkDto.Name,
                checkDto.Endpoint,
                TimeSpan.FromSeconds(checkDto.IntervalSeconds),
                Enum.Parse<CheckType>(checkDto.CheckType, ignoreCase: true),
                checkDto.TimeoutSeconds,
                checkDto.ExpectedResponse);

            foreach (var header in checkDto.Headers)
            {
                updatedCheck.AddHeader(header.Key, header.Value);
            }

            service.AddServiceCheck(updatedCheck);
        }

        await _serviceRepository.UpdateAsync(service);

        //await _eventPublisher.PublishAsync(new ServiceConfigurationChangedEvent
        //{
        //    ServiceId = service.Id,
        //    ServiceName = service.Name,
        //    Timestamp = DateTime.UtcNow
        //});

        return true;
    }
}
