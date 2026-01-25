using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class IsDeleted : BaseValueObject<IsDeleted>
{
    public bool Value { get; private set; }

    private IsDeleted()
    {
    }

    private IsDeleted(bool value)
    {
        Value = value;
    }

    public static IsDeleted True()
    {
        return new IsDeleted(value: true);
    }

    public static IsDeleted False()
    {
        return new IsDeleted(value: false);
    }

    public static IsDeleted FromBoolean(bool value)
    {
        return new IsDeleted(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator IsDeleted(bool value)
    {
        return new IsDeleted(value);
    }

    public static explicit operator bool(IsDeleted isDeleted)
    {
        return isDeleted.Value;
    }
}