using System.ComponentModel.DataAnnotations;

namespace RocketOps.Core.Domain.Options;

public interface IValidateRocketOpsOptions : IValidatableObject
{
    static abstract string ConfigurationKey { get; }
}
