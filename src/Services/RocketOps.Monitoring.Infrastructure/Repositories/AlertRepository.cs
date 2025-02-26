using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.CosmosDb.Repositories;
using RocketOps.Core.Infrastructure.CosmosDb.Services;
using RocketOps.Monitoring.Domain.Models;
using RocketOps.Monitoring.Domain.Repositories;
using System.Net;

namespace RocketOps.Monitoring.Infrastructure.Repositories;

/// <summary>
/// Repository for managing alerts in CosmosDB
/// </summary>
public class AlertRepository : BaseCosmosRepository<Alert>, IAlertRepository
{
    private readonly Container _container;
    private readonly ILogger<AlertRepository> _logger;

    public AlertRepository(
        ICosmosDbClientService cosmosService,
        ILogger<AlertRepository> logger)
        : base(cosmosService, logger)
    {
        _container = cosmosService.GetContainer<Alert>();
        _logger = logger;
    }

    /// <summary>
    /// Get an alert by its ID
    /// </summary>
    public async Task<Alert?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _container.ReadItemAsync<Alert>(
                id.ToString(),
                new PartitionKey(id.ToString()),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    /// <summary>
    /// Get all alerts
    /// </summary>
    public async Task<IEnumerable<Alert>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var query = new QueryDefinition("SELECT * FROM c");
        var results = new List<Alert>();

        var iterator = _container.GetItemQueryIterator<Alert>(query);
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync(cancellationToken);
            results.AddRange(response);
        }

        return results;
    }

    /// <summary>
    /// Get alerts by status
    /// </summary>
    public async Task<IEnumerable<Alert>> GetByStatusAsync(
        string status,
        CancellationToken cancellationToken = default)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.Status = @status")
            .WithParameter("@status", status);

        var results = new List<Alert>();

        var iterator = _container.GetItemQueryIterator<Alert>(query);
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync(cancellationToken);
            results.AddRange(response);
        }

        return results;
    }

    /// <summary>
    /// Add a new alert
    /// </summary>
    public async Task<Alert> AddAsync(
        Alert alert,
        CancellationToken cancellationToken = default)
    {
        var response = await _container.CreateItemAsync(
            alert,
            new PartitionKey(alert.Id.ToString()),
            cancellationToken: cancellationToken);

        return response.Resource;
    }

    /// <summary>
    /// Update an existing alert
    /// </summary>
    public async Task UpdateAsync(
        Alert alert,
        CancellationToken cancellationToken = default)
    {
        await _container.UpsertItemAsync(
            alert,
            new PartitionKey(alert.Id.ToString()),
            cancellationToken: cancellationToken);
    }
}
