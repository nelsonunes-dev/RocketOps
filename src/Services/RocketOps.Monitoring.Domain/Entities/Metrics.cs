namespace RocketOps.Monitoring.Domain.Entities;

/// <summary>
/// Represents a collection of metrics for a service
/// </summary>
public class Metrics
{
    public Guid Id { get; private set; }
    public Guid ServiceId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public double ResponseTimeMs { get; private set; }
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public Dictionary<string, double> CustomMetrics { get; private set; } = new();

    // For serialization
    private Metrics() { }

    public Metrics(
        Guid serviceId,
        double responseTimeMs,
        bool isSuccess,
        string errorMessage = "")
    {
        Id = Guid.NewGuid();
        ServiceId = serviceId;
        Timestamp = DateTime.UtcNow;
        ResponseTimeMs = responseTimeMs;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public void AddCustomMetric(string key, double value)
    {
        CustomMetrics[key] = value;
    }
}
