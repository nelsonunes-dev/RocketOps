using Shared.Domain.Models;

namespace Shared.Infrastructure.Responses;

public class HealthCheckResponse
{
    public string Status { get; set; } = default!;
    public required Dictionary<string, object> Checks { get; set; }
}
