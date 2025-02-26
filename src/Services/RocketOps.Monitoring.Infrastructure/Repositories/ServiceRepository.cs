using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RocketOps.Core.Infrastructure.CosmosDb.Services;
using RocketOps.Monitoring.Domain.Entities;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;
using System.Net;

namespace RocketOps.Monitoring.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly Container _container;
    private readonly ILogger<ServiceRepository> _logger;

    public ServiceRepository(
        ICosmosDbClientService cosmosService,
        ILogger<ServiceRepository> logger)
    {
        _container = cosmosService.GetContainer<Service>();
        _logger = logger;
    }

    public async Task<Service> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _container.ReadItemAsync<Service>(
                id.ToString(),
                new PartitionKey(id.ToString()));

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Service with ID {ServiceId} was not found", id);
            return null;
        }
    }

    public async Task<List<Service>> GetAllAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c");
        return await ExecuteQueryAsync(query);
    }

    public async Task<List<Service>> GetActiveServicesAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.IsActive = true");
        return await ExecuteQueryAsync(query);
    }

    public async Task AddAsync(Service service)
    {
        await _container.CreateItemAsync(
            service,
            new PartitionKey(service.Id.ToString()));
    }

    public async Task UpdateAsync(Service service)
    {
        await _container.UpsertItemAsync(
            service,
            new PartitionKey(service.Id.ToString()));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _container.DeleteItemAsync<Service>(
            id.ToString(),
            new PartitionKey(id.ToString()));
    }

    private async Task<List<Service>> ExecuteQueryAsync(QueryDefinition query)
    {
        var results = new List<Service>();
        var iterator = _container.GetItemQueryIterator<Service>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}
