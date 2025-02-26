using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.CosmosDb.Services;
using RocketOps.Monitoring.Domain.Models;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;
using System.Net;

namespace RocketOps.Monitoring.Infrastructure.Repositories;

public class HealthCheckResultRepository : IHealthCheckResultRepository
{
    private readonly Container _container;
    private readonly ILogger<HealthCheckResultRepository> _logger;

    public HealthCheckResultRepository(
        ICosmosDbClientService cosmosService,
        ILogger<HealthCheckResultRepository> logger)
    {
        _container = cosmosService.GetContainer<HealthCheckResult>();
        _logger = logger;
    }

    public async Task<HealthCheckResult> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _container.ReadItemAsync<HealthCheckResult>(
                id.ToString(),
                new PartitionKey(id.ToString()));

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("HealthCheckResult with ID {HealthCheckId} was not found", id);
            return null;
        }
    }

    public async Task<List<HealthCheckResult>> GetByServiceIdAsync(Guid serviceId)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.ServiceId = @serviceId ORDER BY c.Timestamp DESC")
            .WithParameter("@serviceId", serviceId);

        return await ExecuteQueryAsync(query);
    }

    public async Task<List<HealthCheckResult>> GetRecentByServiceIdAsync(Guid serviceId, int count = 10)
    {
        var query = new QueryDefinition(
            "SELECT TOP @count * FROM c WHERE c.ServiceId = @serviceId ORDER BY c.Timestamp DESC")
            .WithParameter("@serviceId", serviceId)
            .WithParameter("@count", count);

        return await ExecuteQueryAsync(query);
    }

    public async Task AddAsync(HealthCheckResult result)
    {
        await _container.CreateItemAsync(
            result,
            new PartitionKey(result.Id.ToString()));
    }

    private async Task<List<HealthCheckResult>> ExecuteQueryAsync(QueryDefinition query)
    {
        var results = new List<HealthCheckResult>();
        var iterator = _container.GetItemQueryIterator<HealthCheckResult>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}
