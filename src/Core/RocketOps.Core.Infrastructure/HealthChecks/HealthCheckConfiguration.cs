using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RocketOps.Core.Domain.Models;
using RocketOps.Core.Infrastructure.HealthChecks.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketOps.Core.Infrastructure.HealthChecks;

public static class HealthCheckConfiguration
{
    public static IHealthChecksBuilder AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var healthChecksBuilder = services.AddHealthChecks()
            // Basic self health check
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["base"]);

        // Add any application-specific health checks
        // Example: Database health check
        // healthChecksBuilder.AddCosmosDb(configuration);

        // Example: External service health checks
        // healthChecksBuilder.AddUrlGroup(new Uri("https://example.com"), "External Service");

        return healthChecksBuilder;
    }

    public static void MapCustomHealthChecks(this WebApplication app)
    {
        // Detailed health check endpoint
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            AllowCachingResponses = false,
            ResponseWriter = WriteHealthCheckResponse
        });

        // Minimal health check endpoint (just up/down)
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("base"),
            AllowCachingResponses = false,
            ResponseWriter = WriteHealthCheckResponse
        });
    }

    private static Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            TotalDuration = report.TotalDuration,
            Results = report.Entries.Select(entry => new HealthCheck
            {
                Name = entry.Key,
                Status = entry.Value.Status.ToString(),
                Description = entry.Value.Description,
                Duration = entry.Value.Duration.ToString()
            }).ToList()
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }));
    }
}
