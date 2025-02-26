using RocketOps.Monitoring.Domain.Entities;

namespace RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

public interface IMetricsRepository
{
    Task<Metrics> GetByIdAsync(Guid id);
    Task<List<Metrics>> GetByServiceIdAsync(Guid serviceId, DateTime from, DateTime to);
    Task AddAsync(Metrics metrics);
}
