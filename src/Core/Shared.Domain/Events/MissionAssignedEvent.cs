using Shared.Domain.Enums;
using Shared.Domain.Events.Base;

namespace Shared.Domain.Events;

/// <summary>
/// Event for mission assignment
/// </summary>
public class MissionAssignedEvent : DomainEventBase
{
    /// <summary>
    /// Unique identifier of the mission
    /// </summary>
    public Guid MissionId { get; }

    /// <summary>
    /// Unique identifier of the rocket
    /// </summary>
    public Guid RocketId { get; }

    /// <summary>
    /// Mission objective
    /// </summary>
    public string Objective { get; }

    /// <summary>
    /// Mission priority
    /// </summary>
    public MissionPriority Priority { get; }

    public MissionAssignedEvent(Guid missionId, Guid rocketId, string objective, MissionPriority priority)
    {
        MissionId = missionId;
        RocketId = rocketId;
        Objective = objective;
        Priority = priority;
    }
}
