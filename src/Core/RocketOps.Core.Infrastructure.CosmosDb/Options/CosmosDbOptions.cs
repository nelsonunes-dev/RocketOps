using RocketOps.Core.Domain.Options;
using System.ComponentModel.DataAnnotations;

namespace RocketOps.Core.Infrastructure.CosmosDb.Options;

public class CosmosDbOptions : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => "CosmosDb";

    // Primary connection properties
    public string Endpoint { get; set; } = string.Empty;
    public string PrimaryKey { get; set; } = string.Empty;
    public string DatabaseId { get; set; } = "RocketOpsDb";

    // Optional connection string
    public string ConnectionString { get; set; } = string.Empty;

    public int MaxRetryAttempts { get; set; } = 3;
    public int MaxConcurrency { get; set; } = 16;

    // Parse connection string if provided
    public void ParseConnectionString()
    {
        if (!string.IsNullOrEmpty(ConnectionString))
        {
            var parts = ConnectionString.Split(';')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            foreach (var part in parts)
            {
                if (part.StartsWith("AccountEndpoint=", StringComparison.OrdinalIgnoreCase))
                {
                    Endpoint = part.Substring("AccountEndpoint=".Length);
                }
                else if (part.StartsWith("AccountKey=", StringComparison.OrdinalIgnoreCase))
                {
                    PrimaryKey = part.Substring("AccountKey=".Length);
                }
            }
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Parse connection string if provided
        ParseConnectionString();

        if (string.IsNullOrEmpty(ConfigurationKey))
            yield return new ValidationResult("CosmosDB Endpoint is required", [nameof(ConfigurationKey)]);

        if (string.IsNullOrEmpty(Endpoint))
            yield return new ValidationResult("CosmosDB Endpoint is required", [nameof(Endpoint)]);

        if (string.IsNullOrEmpty(PrimaryKey))
            yield return new ValidationResult("CosmosDB PrimaryKey is required", [nameof(PrimaryKey)]);

        if (string.IsNullOrEmpty(DatabaseId))
            yield return new ValidationResult("CosmosDB DatabaseId is required", [nameof(DatabaseId)]);
    }
}