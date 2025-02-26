using RocketOps.Monitoring.Domain.Models;

namespace RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

public interface IHealthCheckResultRepository
{
    Task<HealthCheckResult> GetByIdAsync(Guid id);
    Task<List<HealthCheckResult>> GetByServiceIdAsync(Guid serviceId);
    Task<List<HealthCheckResult>> GetRecentByServiceIdAsync(Guid serviceId, int count = 10);
    Task AddAsync(HealthCheckResult result);
}
