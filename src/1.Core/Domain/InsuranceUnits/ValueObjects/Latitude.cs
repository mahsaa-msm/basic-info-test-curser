using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceUnits.ValueObjects;

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