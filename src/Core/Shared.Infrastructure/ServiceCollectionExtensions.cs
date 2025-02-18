using Shared.Infrastructure.Configurations;

namespace Shared.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilogConfiguration(configuration);
        
        services.AddFastEndpointsConfiguration(configuration);
        services.AddOpenApiConfiguration(configuration);
        
        return services;
    }

    public static WebApplication UseSharedInfrastructureServices(this WebApplication app, IConfiguration configuration)
    {
        app.UseSerilogRequestLogging();

        app.UseFastEndpointsConfiguration(app.Environment);
        app.UseOpenApiConfiguration(configuration);

        return app;
    }

    public static IHostBuilder UseShareInfrastructureHostServices(this IHostBuilder hostBuilder, IConfiguration configuration) 
        => hostBuilder.ConfigureSerilog(configuration);
}
