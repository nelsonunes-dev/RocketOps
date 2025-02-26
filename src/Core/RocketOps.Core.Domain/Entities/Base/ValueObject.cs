using System.Text.Json;

namespace RocketOps.Core.Domain.Entities.Base;

/// <summary>
/// Represents a Value Object in Domain-Driven Design
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Returns the atomic values of the Value Object
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Compares Value Objects
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Generates hash code based on equality components
    /// </summary>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Provides a string representation of the Value Object
    /// </summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
