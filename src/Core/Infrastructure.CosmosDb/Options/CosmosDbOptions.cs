using Shared.Abstractions;

namespace Infrastructure.CosmosDb.Options;

/// <summary>
/// Configuration options for CosmosDB connection
/// </summary>
public class CosmosDbOptions : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => "CosmosDb";

    /// <summary>
    /// Endpoint URL for the CosmosDB account
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Primary key for authentication
    /// </summary>
    public string PrimaryKey { get; set; } = string.Empty;

    /// <summary>
    /// Database identifier
    /// </summary>
    public string DatabaseId { get; set; } = string.Empty;

    /// <summary>
    /// Maximum number of retries for CosmosDB operations
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Maximum parallel degree of concurrency
    /// </summary>
    public int MaxConcurrency { get; set; } = 16;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ConfigurationKey))
        {
            yield return new ValidationResult($"{nameof(CosmosDbOptions)}.{nameof(ConfigurationKey)} is not configured.", [nameof(ConfigurationKey)]);
        }
    }
}
