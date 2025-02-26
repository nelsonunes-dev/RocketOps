using RocketOps.Core.Application.CQRS;

namespace RocketOps.Monitoring.Application.Commands.CreateAlert;

/// <summary>
/// Command for creating a new alert
/// </summary>
public class CreateAlertCommand : ICqrsCommand
{
    /// <summary>
    /// Alert ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Alert name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Alert description
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Alert severity
    /// </summary>
    public string Severity { get; }

    /// <summary>
    /// Service that triggered the alert
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Creates a new alert command
    /// </summary>
    public CreateAlertCommand(string name, string description, string severity, string serviceName)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Severity = severity;
        ServiceName = serviceName;
    }
}