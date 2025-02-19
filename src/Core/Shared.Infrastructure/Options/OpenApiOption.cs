using Shared.Abstractions;

namespace Shared.Infrastructure.Options;

public class OpenApiOption : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => "OpenApi";

    public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}
