using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.ValueObjects;

public sealed class NullablePercentage : BaseValueObject<NullablePercentage>
{
    #region Properties
    public double? Value { get; private set; } = null;
    public bool IsNull { get; }
    #endregion

    #region Constructors
    private NullablePercentage() { }

    public NullablePercentage(double? value)
    {
        IsNull = value is null;

        if (!IsNull)
        {
            ValueObjectGuard.ThrowIfIsNotBetween(value,
                                                 ProjectConsts.PERCENTAGE_MIN_VALUE,
                                                 ProjectConsts.PERCENTAGE_MAX_VALUE,
                                                 ProjectTranslation.PERCENTAGE);
            Value = value;
        }
    }
    #endregion

    #region Commands
    public static NullablePercentage FromDouble(double? value) => new(value);

    public NullablePercentage Increase(double? increasedValue = 1) => !IsNull ? new(Value + increasedValue) : new(increasedValue);

    public NullablePercentage Decrease(double? decreasedValue = 1) => !IsNull ? new(Value - decreasedValue) : new(decreasedValue);
    #endregion

    #region Queries
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value!;
    }

    public override string? ToString()
    {
        return Value?.ToString();
    }
    #endregion

    #region Operators
    public static NullablePercentage operator +(NullablePercentage percent, int value) => percent.Increase(value);

    public static NullablePercentage operator -(NullablePercentage percent, int value) => percent.Decrease(value);

    public static bool operator <(NullablePercentage left, NullablePercentage right) => left.Value < right.Value;

    public static bool operator >(NullablePercentage left, NullablePercentage right) => left.Value > right.Value;

    public static bool operator <=(NullablePercentage left, NullablePercentage right) => left.Value <= right.Value;

    public static bool operator >=(NullablePercentage left, NullablePercentage right) => left.Value >= right.Value;

    public static explicit operator double?(NullablePercentage percent) => percent.Value;

    public static implicit operator NullablePercentage(double? value) => new(value);
    #endregion
}

