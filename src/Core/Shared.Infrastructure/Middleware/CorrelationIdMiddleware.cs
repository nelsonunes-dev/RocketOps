namespace Shared.Infrastructure.Middleware;

/// <summary>
/// Request correlation ID middleware to track requests across services
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;
    private const string CorrelationIdHeaderName = "X-Correlation-ID";

    public CorrelationIdMiddleware(
        RequestDelegate next,
        ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate or use existing correlation ID
        var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault() ?? Guid.NewGuid().ToString();

        // Add to both request and response headers
        context.Request.Headers["X-Correlation-ID"] = correlationId;
        context.Response.Headers["X-Correlation-ID"] = correlationId;

        // Add to logging context using ILogger
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId
        }))
        {
            await _next(context);
        }
    }
}
