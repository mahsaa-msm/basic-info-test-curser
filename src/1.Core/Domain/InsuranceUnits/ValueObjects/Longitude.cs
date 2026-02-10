using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceUnits.ValueObjects;

public sealed class Longitude : BaseValueObject<Longitude>
{
    public double Value { get; }

    public Longitude(double value)
    {
        ValueObjectGuard.ThrowIfIsNotBetween(value,
                                     ProjectConsts.LONGITUDE_MIN_VALUE,
                                     ProjectConsts.LONGITUDE_MAX_VALUE,
                                     ProjectTranslation.LONGITUDE);
        Value = value;
    }

    public static implicit operator double(Longitude longitude) => longitude.Value;
    public static implicit operator Longitude(double value) => new(value);

    public static Longitude FromDouble(double value) => new Longitude(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public double ToDouble() => Value;
}