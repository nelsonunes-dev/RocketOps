using Infrastructure.CosmosDb;
using Shared.Infrastructure.Configurations;
using Shared.Infrastructure.Extensions;

namespace Shared.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilogConfiguration(configuration);
        
        services.AddFastEndpointsConfiguration(configuration);
        services.AddOpenApiConfiguration(configuration);

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());

        services.AddCosmosDb(configuration);

        return services;
    }

    public static WebApplication UseSharedInfrastructureServices(this WebApplication app, IConfiguration configuration)
    {
        app.UseMiddleware();

        app.UseSerilogRequestLogging();

        app.UseFastEndpointsConfiguration(app.Environment);
        app.UseOpenApiConfiguration(configuration);

        return app;
    }

    public static IHostBuilder UseShareInfrastructureHostServices(this IHostBuilder hostBuilder, IConfiguration configuration) 
        => hostBuilder.ConfigureSerilog(configuration);
}
