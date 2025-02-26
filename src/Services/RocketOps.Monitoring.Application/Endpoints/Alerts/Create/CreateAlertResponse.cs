namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Create;

public class CreateAlertResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; } = "Alert created successfully";
}
