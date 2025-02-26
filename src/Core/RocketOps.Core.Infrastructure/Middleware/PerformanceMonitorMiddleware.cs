using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RocketOps.Core.Infrastructure.Middleware;

/// <summary>
/// Performance monitoring middleware to log request processing times
/// </summary>
public class PerformanceMonitorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMonitorMiddleware> _logger;

    public PerformanceMonitorMiddleware(RequestDelegate next, ILogger<PerformanceMonitorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            LogPerformance(context, stopwatch.ElapsedMilliseconds);
        }
    }

    private void LogPerformance(HttpContext context, long elapsedMilliseconds)
    {
        var logLevel = elapsedMilliseconds switch
        {
            >= 1000 => LogLevel.Warning,
            >= 500 => LogLevel.Information,
            _ => LogLevel.Debug
        };

        _logger.Log(logLevel,
            "Request {Method} {Path} completed in {ElapsedMs}ms",
            context.Request.Method,
            context.Request.Path,
            elapsedMilliseconds);
    }
}
