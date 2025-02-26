using RocketOps.Core.Domain.Options;
using System.ComponentModel.DataAnnotations;

namespace RocketOps.Core.Infrastructure.Options;

public class SerilogOption : IValidateRocketOpsOptions
{
    public static string ConfigurationKey => "LogSettings";

    public string ApplicationName { get; set; } = "RocketOps";
    public string MinimumLevel { get; set; } = "Information";
    public string OutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    public bool EnableFileLogging { get; set; } = true;
    public bool EnableConsoleLogging { get; set; } = true;
    public string LogDirectory { get; set; } = "Logs";
    public string LogFileName { get; set; } = "rocketops-.log";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConfigurationKey))
            yield return new ValidationResult($"{nameof(SerilogOption)}.{ConfigurationKey}. is required.", new[] { nameof(ConfigurationKey) });

        if (string.IsNullOrEmpty(ApplicationName))
            yield return new ValidationResult($"{nameof(SerilogOption)}.{ApplicationName}. is required.", new[] { nameof(ApplicationName) });

        if (EnableFileLogging && string.IsNullOrEmpty(LogFileName))
            yield return new ValidationResult($"{nameof(SerilogOption)}.{LogFileName}. is required when file logging is enabled", new[] { nameof(LogFileName) });
    }
}
