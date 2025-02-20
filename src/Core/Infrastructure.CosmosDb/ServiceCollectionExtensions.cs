using Infrastructure.CosmosDb.Options;
using Infrastructure.CosmosDb.Services;

namespace Infrastructure.CosmosDb;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add CosmosDB services to the dependency injection container
    /// </summary>
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind configuration to CosmosDbOptions
        services.Configure<CosmosDbOptions>(configuration.GetSection("CosmosDb"));

        // Register CosmosDB client service
        services.AddSingleton<CosmosDbClientService>();

        return services;
    }
}
