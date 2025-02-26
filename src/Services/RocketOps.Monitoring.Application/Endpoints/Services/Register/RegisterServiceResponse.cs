namespace RocketOps.Monitoring.Application.Endpoints.Services.Register;

public class RegisterServiceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
