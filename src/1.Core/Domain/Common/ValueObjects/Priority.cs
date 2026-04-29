using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.ValueObjects;

public class Priority : BaseValueObject<Priority>
{
    public long Value { get; private set; }

    private Priority()
    {
    }

    public Priority(long value)
    {
        ValueObjectGuard.ThrowIfIsNotGraterOrEqualThan(value,
                                                       ProjectConsts.NATURAL_NUMBER_MIN_VALUE,
                                                       ProjectTranslation.PRIORITY);
        Value = value;
    }

    public static Priority FromLong(long value)
    {
        return new Priority(value);
    }

    public Priority Increase(long increasedValue = 1)
    {
        return new Priority(Value + increasedValue);
    }

    public Priority Decrease(long decreasedValue = 1)
    {
        return new Priority(Value - decreasedValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static Priority operator +(Priority priority, long value)
    {
        return priority.Increase(value);
    }

    public static Priority operator -(Priority priority, long value)
    {
        return priority.Decrease(value);
    }

    public static bool operator <(Priority left, Priority right)
    {
        return left.Value < right.Value;
    }

    public static bool operator >(Priority left, Priority right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(Priority left, Priority right)
    {
        return left.Value <= right.Value;
    }

    public static bool operator >=(Priority left, Priority right)
    {
        return left.Value >= right.Value;
    }

    public static explicit operator long?(Priority priority)
    {
        return priority.Value;
    }

    public static explicit operator long(Priority priority) => priority.Value;

    public static implicit operator Priority(long value) => new(value);

}
