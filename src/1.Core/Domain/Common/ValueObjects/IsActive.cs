using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public sealed class IsActive : BaseValueObject<IsActive>
{
    public bool Value { get; private set; }

    private IsActive()
    {
    }

    public IsActive(bool value)
    {
        Value = value;
    }

    public static IsActive True()
    {
        return new IsActive(value: true);
    }

    public static IsActive False()
    {
        return new IsActive(value: false);
    }

    public static IsActive FromBoolean(bool value)
    {
        return new IsActive(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator IsActive(bool value)
    {
        return new IsActive(value);
    }

    public static explicit operator bool(IsActive isActive)
    {
        return isActive.Value;
    }
}