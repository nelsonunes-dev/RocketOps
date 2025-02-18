using System.ComponentModel.DataAnnotations;

namespace Shared.Abstractions;

public interface IValidateRocketOpsOptions : IValidatableObject
{
    static abstract string ConfigurationKey { get; }
}
