using Shared.Domain.Abstractions;

namespace Shared.Domain.Events.Base;

/// <summary>
/// Base class for domain events with MediatR support
/// </summary>
public abstract class DomainEventBase : IDomainEvent, INotification
{
    /// <summary>
    /// Unique identifier for the event
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Timestamp of the event creation
    /// </summary>
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    /// <summary>
    /// Correlation ID to track event lineage
    /// </summary>
    public Guid? CorrelationId { get; set; }

    /// <summary>
    /// Optional metadata for the event
    /// </summary>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Returns a JSON representation of the event
    /// </summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
