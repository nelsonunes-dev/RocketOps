using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocketOps.Core.Infrastructure.CosmosDb.HealthChecks;
using RocketOps.Core.Infrastructure.CosmosDb.Options;
using RocketOps.Core.Infrastructure.CosmosDb.Services;

namespace RocketOps.Core.Infrastructure.CosmosDb;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add CosmosDB services to the dependency injection container
    /// </summary>
    // In RocketOps.Core.Infrastructure.CosmosDb/ServiceCollectionExtensions.cs
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration, bool healthChecksRegistered = false)
    {
        // Configure CosmosDB options
        services.Configure<CosmosDbOptions>(options =>
        {
            // Bind from configuration section
            configuration.GetSection("CosmosDb").Bind(options);

            // Check for connection string in environment or configuration
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__CosmosDb")
                ?? configuration.GetConnectionString("CosmosDb");

            if (!string.IsNullOrEmpty(connectionString))
            {
                options.ConnectionString = connectionString;
                options.ParseConnectionString();
            }

            // Check for DatabaseId in environment or configuration
            var dbId = Environment.GetEnvironmentVariable("CosmosDb__DatabaseId")
                ?? configuration["CosmosDb:DatabaseId"];

            if (!string.IsNullOrEmpty(dbId))
            {
                options.DatabaseId = dbId;
            }
        });

        // Register CosmosDB client
        services.AddSingleton<ICosmosDbClientService, CosmosDbClientService>();

        // Add health check only if not already registered
        if (!healthChecksRegistered)
        {
            services.AddHealthChecks()
                    .AddCheck<CosmosDbHealthCheck>("cosmosdb");
        }

        return services;
    }
}
