using Infrastructure.CosmosDb.Options;

namespace Infrastructure.CosmosDb.Services;

/// <summary>
/// CosmosDB client service for managing database connections
/// </summary>
public class CosmosDbClientService
{
    private readonly CosmosClient _client;
    private readonly Database _database;
    private readonly ILogger<CosmosDbClientService> _logger;
    private readonly IAsyncPolicy _retryPolicy;

    /// <summary>
    /// Creates a new CosmosDB client service
    /// </summary>
    public CosmosDbClientService(IOptions<CosmosDbOptions> options, ILogger<CosmosDbClientService> logger)
    {
        var cosmosOptions = options.Value;

        // Configure retry policy
        _retryPolicy = Policy
            .Handle<CosmosException>(ex =>
                ex.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                cosmosOptions.MaxRetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            );

        // Create Cosmos client with custom options
        _client = new CosmosClient(
            cosmosOptions.Endpoint,
            cosmosOptions.PrimaryKey,
            new CosmosClientOptions()
        );

        _database = _client.GetDatabase(cosmosOptions.DatabaseId);
        _logger = logger;

        _logger.LogInformation("CosmosDB client initialized for database {DatabaseId}",
            cosmosOptions.DatabaseId);
    }

    /// <summary>
    /// Get a container from the database
    /// </summary>
    public Container GetContainer(string containerId)
    {
        return _database.GetContainer(containerId);
    }

    /// <summary>
    /// Create a container if it doesn't exist
    /// </summary>
    public async Task<Container> CreateContainerIfNotExistsAsync(string containerId, string partitionKeyPath, int? throughput = null)
    {
        _logger.LogInformation("Creating container {ContainerId} if not exists", containerId);

        var containerProperties = new ContainerProperties(containerId, partitionKeyPath);

        return await _database.CreateContainerIfNotExistsAsync(
            containerProperties,
            throughput != null ? ThroughputProperties.CreateManualThroughput(throughput.Value) : null
        );
    }

    /// <summary>
    /// Execute an operation with retry policy
    /// </summary>
    public async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation)
    {
        return await _retryPolicy.ExecuteAsync(operation);
    }

    /// <summary>
    /// Performs a CosmosDB operation with retry and logging
    /// </summary>
    public async Task<T> ExecuteCosmosOperationAsync<T>(Func<Task<T>> operation)
    {
        try
        {
            return await ExecuteWithRetryAsync(operation);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "CosmosDB operation failed after retries");
            throw;
        }
    }

    /// <summary>
    /// Dispose the Cosmos client
    /// </summary>
    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
