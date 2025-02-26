using MediatR;
using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Application.Commands.UpdateHealthStatus;

public class UpdateHealthStatusCommand : IRequest<bool>
{
    public Guid ServiceId { get; set; }
    public HealthStatus Status { get; set; }
    public double ResponseTimeMs { get; set; }
    public string Message { get; set; } = default!;
    public Guid? ServiceCheckId { get; set; }
}
