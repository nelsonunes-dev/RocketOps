using Microsoft.Azure.Cosmos;
using RocketOps.Core.Domain.Entities.Base;
using RocketOps.Core.Domain.Enums;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketOps.Core.Data.Utilities;

/// <summary>
/// Utility class for data access operations
/// </summary>
public static class DataAccessUtilities
{
    /// <summary>
    /// Default JSON serialization options
    /// </summary>
    public static JsonSerializerOptions DefaultJsonOptions { get; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    /// <summary>
    /// Serialize an object to JSON
    /// </summary>
    public static string SerializeToJson<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, DefaultJsonOptions);
    }

    /// <summary>
    /// Deserialize JSON to an object
    /// </summary>
    public static T? DeserializeFromJson<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultJsonOptions);
    }

    /// <summary>
    /// Create a patch operation for updating specific properties
    /// </summary>
    public static List<PatchOperation> CreatePatchOperations<T>(
        T originalEntity,
        T updatedEntity)
    {
        var patchOperations = new List<PatchOperation>();
        var properties = typeof(T).GetProperties()
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var prop in properties)
        {
            var originalValue = prop.GetValue(originalEntity);
            var updatedValue = prop.GetValue(updatedEntity);

            if (!Equals(originalValue, updatedValue))
            {
                patchOperations.Add(PatchOperation.Replace($"/{prop.Name}", updatedValue));
            }
        }

        return patchOperations;
    }

    /// <summary>
    /// Create a dynamic query predicate
    /// </summary>
    public static Expression<Func<T, bool>> CreateDynamicPredicate<T>(
        Dictionary<string, object> filterCriteria)
    {
        if (filterCriteria == null || filterCriteria.Count == 0)
        {
            return x => true;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combinedExpression = null;

        foreach (var criteria in filterCriteria)
        {
            var property = Expression.Property(parameter, criteria.Key);
            var constant = Expression.Constant(criteria.Value);
            var equalExpression = Expression.Equal(property, constant);

            combinedExpression = combinedExpression == null ? equalExpression : Expression.AndAlso(combinedExpression, equalExpression);
        }

        return Expression.Lambda<Func<T, bool>>(combinedExpression ?? Expression.Constant(true), parameter);
    }

    /// <summary>
    /// Batch operation utility
    /// </summary>
    public static async Task<List<T>> BatchOperationAsync<T>(
        Container container,
        List<T> items,
        BatchOperationType operationType) where T : Entity
    {
        var results = new List<T>();
        var batches = SplitIntoBatches(items, 100);

        foreach (var batch in batches)
        {
            var transactionalBatch = container.CreateTransactionalBatch(new PartitionKey(batch.First().Id.ToString()));

            foreach (var item in batch)
            {
                switch (operationType)
                {
                    case BatchOperationType.Create:
                        transactionalBatch.CreateItem(item);
                        break;
                    case BatchOperationType.Upsert:
                        transactionalBatch.UpsertItem(item);
                        break;
                    case BatchOperationType.Replace:
                        transactionalBatch.ReplaceItem(item.Id.ToString(), item);
                        break;
                    case BatchOperationType.Delete:
                        transactionalBatch.DeleteItem(item.Id.ToString());
                        break;
                }
            }

            var batchResponse = await transactionalBatch.ExecuteAsync();

            // Collect successful items
            for (int i = 0; i < batchResponse.Count; i++)
            {
                if (batchResponse[i].IsSuccessStatusCode)
                {
                    results.Add(batch[i]);
                }
            }
        }

        return results;
    }

    /// <summary>
    /// Split a list into batches
    /// </summary>
    private static List<List<T>> SplitIntoBatches<T>(List<T> source, int batchSize)
    {
        var batches = new List<List<T>>();
        for (int i = 0; i < source.Count; i += batchSize)
        {
            batches.Add(source.GetRange(i, Math.Min(batchSize, source.Count - i)));
        }
        return batches;
    }
}
