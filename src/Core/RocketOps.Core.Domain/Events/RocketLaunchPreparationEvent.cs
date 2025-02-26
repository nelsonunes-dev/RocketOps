using RocketOps.Core.Domain.Events.Base;

namespace RocketOps.Core.Domain.Events;

/// <summary>
/// Event for rocket launch preparation
/// </summary>
public class RocketLaunchPreparationEvent : DomainEventBase
{
    /// <summary>
    /// Unique identifier of the rocket
    /// </summary>
    public Guid RocketId { get; }

    /// <summary>
    /// Launch site
    /// </summary>
    public string LaunchSite { get; }

    /// <summary>
    /// Estimated launch time
    /// </summary>
    public DateTime EstimatedLaunchTime { get; }

    public RocketLaunchPreparationEvent(Guid rocketId, string launchSite, DateTime estimatedLaunchTime)
    {
        RocketId = rocketId;
        LaunchSite = launchSite;
        EstimatedLaunchTime = estimatedLaunchTime;
    }
}
