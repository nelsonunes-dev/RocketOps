using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocketOps.Monitoring.Domain.Repositories;
using RocketOps.Monitoring.Infrastructure.Repositories;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMonitoringInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register repositories
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IMetricsRepository, MetricsRepository>();
        services.AddScoped<IHealthCheckResultRepository, HealthCheckResultRepository>();

        return services;
    }
}
