using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Domain.Entities;

/// <summary>
/// Represents a health check configuration for a service
/// </summary>
public class ServiceCheck
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Endpoint { get; private set; }
    public TimeSpan Interval { get; private set; }
    public int TimeoutSeconds { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public Dictionary<string, string> Headers { get; private set; } = new();
    public CheckType CheckType { get; private set; }
    public string ExpectedResponse { get; private set; }

    // For serialization
    private ServiceCheck() { }

    public ServiceCheck(string name, string endpoint, TimeSpan interval, CheckType checkType, int timeoutSeconds = 30, string expectedResponse = "")
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        Interval = interval;
        TimeoutSeconds = timeoutSeconds;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
        LastUpdated = DateTime.UtcNow;
        CheckType = checkType;
        ExpectedResponse = expectedResponse;
    }

    public void Update(string name, string endpoint, TimeSpan interval, CheckType checkType, int timeoutSeconds, string expectedResponse)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        Interval = interval;
        TimeoutSeconds = timeoutSeconds;
        CheckType = checkType;
        ExpectedResponse = expectedResponse;
        LastUpdated = DateTime.UtcNow;
    }

    public void AddHeader(string key, string value)
    {
        Headers[key] = value;
        LastUpdated = DateTime.UtcNow;
    }

    public void RemoveHeader(string key)
    {
        if (Headers.ContainsKey(key))
        {
            Headers.Remove(key);
            LastUpdated = DateTime.UtcNow;
        }
    }

    public void Activate()
    {
        IsActive = true;
        LastUpdated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        LastUpdated = DateTime.UtcNow;
    }
}
