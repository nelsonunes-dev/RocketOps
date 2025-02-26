using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using RocketOps.Core.Infrastructure.Configurations;
using RocketOps.Core.Infrastructure.CosmosDb;
using RocketOps.Core.Infrastructure.HealthChecks;
using RocketOps.Core.Infrastructure.Middleware;
using Serilog;

namespace RocketOps.Core.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // More robust check for health checks
        var healthChecksRegistered = services.Any(s => s.ServiceType == typeof(HealthCheckService));

        // Add CosmosDB only if not already added
        services.AddCosmosDb(configuration, healthChecksRegistered);

        // Add health checks
        if (!healthChecksRegistered)
        {
            services.AddHealthChecks()
                    .AddCheck<MicroservicesHealthCheck>("microservice");
        }

        // Add FastEndpoints configuration
        services.AddFastEndpointsConfiguration(configuration);

        // Add OpenAPI configuration 
        services.AddOpenApiConfiguration(configuration);

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructureServices(this IApplicationBuilder app)
    {
        // Add middleware in the correct order
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<PerformanceMonitorMiddleware>();

        return app;
    }

    public static IHostBuilder UseCoreInfrastructureHostServices(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        // Configure Serilog
        hostBuilder.UseSerilog((context, services, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration)
                  .ReadFrom.Services(services)
                  .Enrich.FromLogContext();
        });

        return hostBuilder;
    }
}
