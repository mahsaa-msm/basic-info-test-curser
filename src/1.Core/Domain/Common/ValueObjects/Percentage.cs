using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.ValueObjects;

public sealed class Percentage : BaseValueObject<Percentage>
{
    public double Value { get; private set; }

    private Percentage()
    {
    }

    public Percentage(double value)
    {
        ValueObjectGuard.ThrowIfIsNotBetween(value,
                                             ProjectConsts.PERCENTAGE_MIN_VALUE,
                                             ProjectConsts.PERCENTAGE_MAX_VALUE,
                                             ProjectTranslation.PERCENTAGE);

        Value = value;
    }

    public static Percentage FromDouble(double value)
    {
        return new Percentage(value);
    }

    public Percentage Add(double addedValue)
    {
        return new Percentage(Value + addedValue);
    }

    public Percentage Subtract(double subtractedValue)
    {
        return new Percentage(Value - subtractedValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public double ToDouble()
    {
        return Value;
    }

    public static Percentage operator +(Percentage percentage, double value)
    {
        return percentage.Add(value);
    }

    public static Percentage operator -(Percentage percentage, double value)
    {
        return percentage.Subtract(value);
    }

    public static bool operator <(Percentage left, Percentage right)
    {
        return left.Value < right.Value;
    }

    public static bool operator >(Percentage left, Percentage right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(Percentage left, Percentage right)
    {
        return left.Value <= right.Value;
    }

    public static bool operator >=(Percentage left, Percentage right)
    {
        return left.Value >= right.Value;
    }

    public static explicit operator double?(Percentage percentage)
    {
        return percentage.Value;
    }

    public static explicit operator double(Percentage percentage) => percentage.Value;

    public static implicit operator Percentage(double value) => new(value);
}

