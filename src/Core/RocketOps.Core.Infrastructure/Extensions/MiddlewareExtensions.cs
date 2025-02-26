using Microsoft.AspNetCore.Builder;
using RocketOps.Core.Infrastructure.Middleware;

namespace RocketOps.Core.Infrastructure.Extensions;

/// <summary>
/// Extension methods for registering middleware
/// </summary>
internal static class MiddlewareExtensions
{
    /// <summary>
    /// Adds global exception handling middleware
    /// </summary>
    internal static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder application)
    {
        return application.UseMiddleware<GlobalExceptionMiddleware>();
    }

    /// <summary>
    /// Adds performance monitoring middleware
    /// </summary>
    internal static IApplicationBuilder UsePerformanceMonitoring(this IApplicationBuilder application)
    {
        return application.UseMiddleware<PerformanceMonitorMiddleware>();
    }

    /// <summary>
    /// Adds correlation ID middleware
    /// </summary>
    internal static IApplicationBuilder UseCorrelationId(this IApplicationBuilder application)
    {
        return application.UseMiddleware<CorrelationIdMiddleware>();
    }

    internal static IApplicationBuilder UseMiddleware(this IApplicationBuilder application)
    {
        return application.UseCorrelationId()
            .UsePerformanceMonitoring()
            .UseGlobalExceptionHandling();
    }
}
