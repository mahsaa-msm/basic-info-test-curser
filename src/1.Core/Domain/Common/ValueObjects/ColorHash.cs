using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Core.Resources.Utils.Extensions;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.ValueObjects;

public class ColorHash : BaseValueObject<ColorHash>
{
    public string Value { get; private set; }

    private ColorHash()
    {
    }

    private ColorHash(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.COLOR_HASH);
        value = value.RemoveExcessWhiteSpace();
        ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                         ProjectConsts.COLOR_HASH_MIN_LENGTH,
                                                         ProjectConsts.COLOR_HASH_MAX_LENGTH,
                                                         ProjectTranslation.COLOR_HASH);
        Value = value;
    }

    public static ColorHash FromString(string value)
    {
        return new ColorHash(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator ColorHash(string value)
    {
        return new ColorHash(value);
    }

    public static explicit operator string(ColorHash colorHash)
    {
        return colorHash.Value;
    }
}
