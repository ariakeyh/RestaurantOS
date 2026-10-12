namespace Domain.ValueObjects;

public abstract class ValueObject<T> where T : new()
{
    protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return right != null && left != null && (ReferenceEquals(left, right) || left.Equals(right));
    }

    protected static bool NotEqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj?.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject<T>)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }
    // Other utility methods
}