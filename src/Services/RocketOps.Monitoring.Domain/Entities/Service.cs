using RocketOps.Monitoring.Domain.Enums;

namespace RocketOps.Monitoring.Domain.Entities;

/// <summary>
/// Represents a service that is being monitored
/// </summary>
public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string BaseUrl { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime DateRegistered { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public List<ServiceCheck> ServiceChecks { get; private set; } = new();
    public HealthStatus CurrentHealth { get; private set; }

    // For serialization
    private Service() { }

    public Service(string name, string baseUrl, string description = "")
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        Description = description;
        IsActive = true;
        DateRegistered = DateTime.UtcNow;
        LastUpdated = DateTime.UtcNow;
        CurrentHealth = HealthStatus.Unknown;
    }

    public void Update(string name, string baseUrl, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        Description = description;
        LastUpdated = DateTime.UtcNow;
    }

    public void AddServiceCheck(ServiceCheck check)
    {
        ServiceChecks.Add(check);
        LastUpdated = DateTime.UtcNow;
    }

    public void RemoveServiceCheck(Guid checkId)
    {
        var check = ServiceChecks.Find(c => c.Id == checkId);
        if (check != null)
        {
            ServiceChecks.Remove(check);
            LastUpdated = DateTime.UtcNow;
        }
    }

    public void UpdateHealth(HealthStatus health)
    {
        CurrentHealth = health;
        LastUpdated = DateTime.UtcNow;
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
