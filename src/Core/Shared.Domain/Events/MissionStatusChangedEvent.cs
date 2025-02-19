using Shared.Domain.Enums;
using Shared.Domain.Events.Base;

namespace Shared.Domain.Events;

/// <summary>
/// Event for mission status change
/// </summary>
public class MissionStatusChangedEvent : DomainEventBase
{
    /// <summary>
    /// Unique identifier of the mission
    /// </summary>
    public Guid MissionId { get; }

    /// <summary>
    /// Previous mission status
    /// </summary>
    public MissionStatus PreviousStatus { get; }

    /// <summary>
    /// New mission status
    /// </summary>
    public MissionStatus NewStatus { get; }

    public MissionStatusChangedEvent(Guid missionId, MissionStatus previousStatus, MissionStatus newStatus)
    {
        MissionId = missionId;
        PreviousStatus = previousStatus;
        NewStatus = newStatus;
    }
}
