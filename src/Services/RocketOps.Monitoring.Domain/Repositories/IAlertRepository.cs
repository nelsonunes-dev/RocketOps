using RocketOps.Monitoring.Domain.Models;

namespace RocketOps.Monitoring.Domain.Repositories;

/// <summary>
/// Repository interface for managing alerts
/// </summary>
public interface IAlertRepository
{
    /// <summary>
    /// Get an alert by its ID
    /// </summary>
    Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all alerts
    /// </summary>
    Task<IEnumerable<Alert>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get alerts by status
    /// </summary>
    Task<IEnumerable<Alert>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a new alert
    /// </summary>
    Task<Alert> AddAsync(Alert alert, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing alert
    /// </summary>
    Task UpdateAsync(Alert alert, CancellationToken cancellationToken = default);
}
