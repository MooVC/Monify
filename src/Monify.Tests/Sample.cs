namespace Monify;

internal sealed class Sample
    : IEquatable<Sample>,
      IEquatable<string>
{
    private readonly string _value;

    private Sample(string value)
    {
        _value = value;
    }

    public static implicit operator Sample(string value)
    {
        return new Sample(value);
    }

    public static implicit operator string(Sample sample)
    {
        ArgumentNullException.ThrowIfNull(sample);

        return sample._value;
    }

    public static bool operator ==(Sample? left, Sample? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Sample? left, Sample? right)
    {
        return !(left == right);
    }

    public static bool operator ==(Sample? left, string? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Sample? left, string? right)
    {
        return !(left == right);
    }

    public bool Equals(Sample? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Equals(other._value);
    }

    public bool Equals(string? other)
    {
        return global::System.Collections.Generic.EqualityComparer<string>.Default.Equals(_value, other);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Sample);
    }

    public override int GetHashCode()
    {
        if (_value is null)
        {
            return 0;
        }

        return _value.GetHashCode();
    }
}