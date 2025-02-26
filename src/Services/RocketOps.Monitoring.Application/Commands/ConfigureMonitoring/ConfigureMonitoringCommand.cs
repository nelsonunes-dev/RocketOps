using MediatR;
using RocketOps.Monitoring.Application.Dtos;

namespace RocketOps.Monitoring.Application.Commands.ConfigureMonitoring;

public class ConfigureMonitoringCommand : IRequest<bool>
{
    public Guid ServiceId { get; set; }
    public List<ServiceCheckDto> ChecksToAdd { get; set; } = new();
    public List<Guid> ChecksToRemove { get; set; } = new();
    public List<ServiceCheckUpdateDto> ChecksToUpdate { get; set; } = new();
}

public class ServiceCheckUpdateDto : ServiceCheckDto
{
    public Guid Id { get; set; }
}
