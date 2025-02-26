using System.Text.Json;

namespace RocketOps.Core.Domain.Models;

/// <summary>
/// Error details for standardized error response
/// </summary>
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorId { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
