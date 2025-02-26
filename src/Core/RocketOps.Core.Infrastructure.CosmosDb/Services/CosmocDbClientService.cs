using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RocketOps.Core.Infrastructure.CosmosDb.Options;

namespace RocketOps.Core.Infrastructure.CosmosDb.Services;

/// <summary>
/// CosmosDB client service for managing database connections
/// </summary>
public class CosmosDbClientService : ICosmosDbClientService
{
    private readonly CosmosClient _client;
    private readonly CosmosDbOptions _options;
    private readonly ILogger<CosmosDbClientService> _logger;
    private readonly Database _database;

    public CosmosDbClientService(IOptions<CosmosDbOptions> options, ILogger<CosmosDbClientService> logger)
    {
        _options = options.Value;
        _logger = logger;

        if (string.IsNullOrEmpty(_options.Endpoint))
            throw new ArgumentException("CosmosDB Endpoint is required");

        if (string.IsNullOrEmpty(_options.PrimaryKey))
            throw new ArgumentException("CosmosDB PrimaryKey is required");

        if (string.IsNullOrEmpty(_options.DatabaseId))
            throw new ArgumentException("CosmosDB DatabaseId is required");

        _client = new CosmosClient(
            _options.Endpoint,
            _options.PrimaryKey,
            new CosmosClientOptions
            {
                MaxRetryAttemptsOnRateLimitedRequests = _options.MaxRetryAttempts,
                // Remove MaxConcurrency as it's not available
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

        _database = _client.GetDatabase(_options.DatabaseId);
    }

    public Container GetContainer<T>()
    {
        var containerName = GetContainerNameForType<T>();
        return GetContainer(containerName);
    }

    public Container GetContainer(string containerName)
    {
        return _database.GetContainer(containerName);
    }

    public async Task<bool> HealthCheckAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Simple health check - attempt to read database properties
            await _database.ReadAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "CosmosDB health check failed");
            return false;
        }
    }

    private string GetContainerNameForType<T>()
    {
        // Get container name from type - use the type name as container name
        // In a real implementation, this could use attributes or configuration
        return typeof(T).Name;
    }
}
