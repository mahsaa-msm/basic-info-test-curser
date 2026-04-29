using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.InsuranceUnits.ValueObjects;

public sealed class Latitude : BaseValueObject<Latitude>
{
    public double Value { get; }

    public Latitude(double value)
    {
        ValueObjectGuard.ThrowIfIsNotBetween(value,
                                             ProjectConsts.LATITUDE_MIN_VALUE,
                                             ProjectConsts.LATITUDE_MAX_VALUE,
                                             ProjectTranslation.LATITUDE);
        Value = value;
    }

    public static implicit operator double(Latitude latitude) => latitude.Value;
    public static implicit operator Latitude(double value) => new(value);

    public static Latitude FromDouble(double value)
    {
        return new Latitude(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public double ToDouble()
    {
        return Value;
    }
}
