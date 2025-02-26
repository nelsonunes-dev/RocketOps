using RocketOps.Core.Domain.Options;
using System.ComponentModel.DataAnnotations;

namespace RocketOps.Core.Infrastructure.Options;

public class OpenApiOption : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => "OpenApi";

    public string Title { get; set; } = "RocketOps API";
    public string Description { get; set; } = "RocketOps API Documentation";
    public string Version { get; set; } = "v1";
    public bool EnableSwaggerInProduction { get; set; } = false;
    public string SwaggerRoute { get; set; } = "swagger";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConfigurationKey))
            yield return new ValidationResult($"{nameof(OpenApiOption)}.{ConfigurationKey}. is required.", new[] { nameof(ConfigurationKey) });

        if (string.IsNullOrEmpty(Title))
            yield return new ValidationResult($"{nameof(OpenApiOption)}.{Title}. is required.", new[] { nameof(Title) });

        if (string.IsNullOrEmpty(Version))
            yield return new ValidationResult($"{nameof(OpenApiOption)}.{Version}. is required.", new[] { nameof(Version) });
    }
}
