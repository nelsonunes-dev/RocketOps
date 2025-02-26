using RocketOps.Monitoring.Domain.Entities;

namespace RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

public interface IServiceRepository
{
    Task<Service> GetByIdAsync(Guid id);
    Task<List<Service>> GetAllAsync();
    Task<List<Service>> GetActiveServicesAsync();
    Task AddAsync(Service service);
    Task UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}
