using Shared.Abstractions;

namespace Shared.Infrastructure.Options;

public class SerilogOption : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => throw new NotImplementedException();

    public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}
