namespace RocketOps.Monitoring.Domain.Enums;

/// <summary>
/// Type of check to perform
/// </summary>
public enum CheckType
{
    HttpGet,
    HttpPost,
    Tcp,
    Icmp
}