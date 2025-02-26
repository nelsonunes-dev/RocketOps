using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.CosmosDb.Services;
using RocketOps.Monitoring.Domain.Entities;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;
using System.Net;

namespace RocketOps.Monitoring.Infrastructure.Repositories;

public class MetricsRepository : IMetricsRepository
{
    private readonly Container _container;
    private readonly ILogger<MetricsRepository> _logger;

    public MetricsRepository(
        ICosmosDbClientService cosmosService,
        ILogger<MetricsRepository> logger)
    {
        _container = cosmosService.GetContainer<Metrics>();
        _logger = logger;
    }

    public async Task<Metrics> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _container.ReadItemAsync<Metrics>(
                id.ToString(),
                new PartitionKey(id.ToString()));

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Metrics with ID {MetricsId} was not found", id);
            return null;
        }
    }

    public async Task<List<Metrics>> GetByServiceIdAsync(Guid serviceId, DateTime from, DateTime to)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.ServiceId = @serviceId AND c.Timestamp >= @from AND c.Timestamp <= @to ORDER BY c.Timestamp DESC")
            .WithParameter("@serviceId", serviceId)
            .WithParameter("@from", from)
            .WithParameter("@to", to);

        return await ExecuteQueryAsync(query);
    }

    public async Task AddAsync(Metrics metrics)
    {
        await _container.CreateItemAsync(
            metrics,
            new PartitionKey(metrics.Id.ToString()));
    }

    private async Task<List<Metrics>> ExecuteQueryAsync(QueryDefinition query)
    {
        var results = new List<Metrics>();
        var iterator = _container.GetItemQueryIterator<Metrics>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}
