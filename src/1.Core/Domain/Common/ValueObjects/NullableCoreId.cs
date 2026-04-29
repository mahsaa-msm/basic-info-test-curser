using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Core.Resources.Utils.Extensions;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.ValueObjects;

public class NullableCoreId : BaseValueObject<NullableCoreId>
{
    #region Properties
    public string? Value { get; private set; }
    public bool IsNull { get; }
    #endregion

    #region Constructors
    private NullableCoreId() { }

    public NullableCoreId(string? value)
    {
        IsNull = string.IsNullOrWhiteSpace(value);

        if (!IsNull)
        {
            value = value?.RemoveExcessWhiteSpace();

            ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                             ProjectConsts.CORE_ID_MIN_LENGTH,
                                                             ProjectConsts.CORE_ID_MAX_LENGTH,
                                                             ProjectTranslation.CORE_ID);

            Value = value;
        }

    }
    #endregion

    #region Commands
    public static NullableCoreId FromString(string? value) => value;

    public static NullableCoreId FromInt(int value) => value;

    public static NullableCoreId FromLong(long value) => value;
    #endregion

    #region Queries
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsNull;
        yield return Value;
    }
    public override string? ToString()
    {
        return Value?.ToString();
    }
    #endregion

    #region Operators
    public static explicit operator string?(NullableCoreId coreId) => coreId?.Value;

    public static implicit operator NullableCoreId(string? value) => new(value);

    public static implicit operator NullableCoreId(int value) => new(value.ToString());

    public static implicit operator NullableCoreId(long value) => new(value.ToString());
    #endregion
}
