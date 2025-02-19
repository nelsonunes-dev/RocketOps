namespace Shared.Domain.Models;

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
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}
