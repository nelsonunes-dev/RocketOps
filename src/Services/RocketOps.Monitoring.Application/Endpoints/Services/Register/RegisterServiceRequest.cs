namespace RocketOps.Monitoring.Application.Endpoints.Services.Register;

public class RegisterServiceRequest
{
    public string Name { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
