namespace RocketOps.Monitoring.Application.Dtos;

public class ServiceCheckDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
    public int IntervalSeconds { get; set; }
    public int TimeoutSeconds { get; set; }
    public bool IsActive { get; set; }
    public string CheckType { get; set; } = default!;
    public string ExpectedResponse { get; set; } = default!;
    public Dictionary<string, string> Headers { get; set; } = new();
}
