using RocketOps.Core.Domain.Entities.Base;

namespace RocketOps.Monitoring.Domain.Models;

/// <summary>
/// Represents an alert in the monitoring system
/// </summary>
public class Alert : Entity
{
    /// <summary>
    /// Alert name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Alert description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Alert severity (Low, Medium, High, Critical)
    /// </summary>
    public string Severity { get; set; } = "Medium";

    /// <summary>
    /// Service that triggered the alert
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Alert status (New, Acknowledged, Resolved)
    /// </summary>
    public string Status { get; set; } = "New";

    /// <summary>
    /// Creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Acknowledgement timestamp
    /// </summary>
    public DateTime? AcknowledgedAt { get; set; }

    /// <summary>
    /// Resolution timestamp
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// User who acknowledged the alert
    /// </summary>
    public string? AcknowledgedBy { get; set; }

    /// <summary>
    /// User who resolved the alert
    /// </summary>
    public string? ResolvedBy { get; set; }
}