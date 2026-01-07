using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class Name : BaseValueObject<Name>
{
    public string Value { get; private set; }

    public static Name FromString(string value)
    {
        return new Name(value);
    }

    private Name(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.NAME);

        ValueObjectGuard.ThrowIfIsNotBetween(value.Length,
                                             ProjectConsts.NAME_MIN_LENGTH,
                                             ProjectConsts.NAME_MAX_LENGTH,
                                             ProjectTranslation.NAME);

        Value = value;
    }

    private Name()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator string(Name name)
    {
        return name.Value;
    }

    public static implicit operator Name(string value)
    {
        return new Name(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
