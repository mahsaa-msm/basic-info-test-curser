using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;
public class Priority : BaseValueObject<Priority>
{
    public int Value { get; private set; }

    private Priority()
    {
    }

    public Priority(int value)
    {
        ValueObjectGuard.ThrowIfIsNotGraterOrEqualThan(value,
                                                       ProjectConsts.NATURAL_NUMBER_MIN_VALUE,
                                                       ProjectTranslation.PRIORITY);
        Value = value;
    }

    public static Priority FromInt(int value)
    {
        return new Priority(value);
    }

    public Priority Increase(int increasedValue = 1)
    {
        return new Priority(Value + increasedValue);
    }

    public Priority Decrease(int decreasedValue = 1)
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

    public static Priority operator +(Priority priority, int value)
    {
        return priority.Increase(value);
    }

    public static Priority operator -(Priority priority, int value)
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

    public static explicit operator int?(Priority priority)
    {
        return priority.Value;
    }

    public static implicit operator Priority(int value)
    {
        return new Priority(value);
    }
}