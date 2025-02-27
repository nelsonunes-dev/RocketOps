using RocketOps.Core.Infrastructure;
using RocketOps.Monitoring.Application;
using RocketOps.Monitoring.Infrastructure;

namespace RocketOps.Monitoring.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMonitoringApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Core services first
        services.AddCoreInfrastructureServices(configuration);

        // Then add monitoring-specific services
        services.AddMonitoringApplicationServices();
        services.AddMonitoringInfrastructureServices(configuration);

        return services;
    }

    public static WebApplication UseMonitoringApiServices(this WebApplication app)
    {
        app.UseRouting();
        app.UseCoreInfrastructureServices();

        return app;
    }
}
