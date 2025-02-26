using RocketOps.Core.Domain.Enums;
using RocketOps.Core.Domain.Events.Base;

namespace RocketOps.Core.Domain.Events;

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
