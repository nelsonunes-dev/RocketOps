namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Create;

public class CreateAlertRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string ServiceName { get; set; } = "System"; // Default value
}
