namespace ATS.MVP.Domain.Common.Models;

/// Optei por gerar na mão, ao invés de utilizar alguma lib só para mostrar algumas skills kkk
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        var ValueObject = (ValueObject) other;

        return GetEqualityComponents().SequenceEqual(ValueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject value1, ValueObject value2)
    {
        return Equals(value1, value2);
    }

    public static bool operator !=(ValueObject value1, ValueObject value2)
    {
        return !Equals(value1, value2);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other)
    {
        return Equals((object?) other);
    }
}
