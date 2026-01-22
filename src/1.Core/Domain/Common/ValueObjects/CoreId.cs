using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class CoreId : BaseValueObject<CoreId>
{
    public string Value { get; private set; } = string.Empty;

    private CoreId()
    {
    }

    public CoreId(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.CORE_ID);
        value = value.RemoveExcessWhiteSpace();
        ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                         ProjectConsts.CORE_ID_MIN_LENGTH,
                                                         ProjectConsts.CORE_ID_MAX_LENGTH,
                                                         ProjectTranslation.CORE_ID);
        Value = value;
    }

    public static CoreId FromString(string value)
    {
        return value;
    }

    public static CoreId FromInt(int value)
    {
        return value;
    }

    public static CoreId FromLong(long value)
    {
        return value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static explicit operator string(CoreId coreId)
    {
        return coreId.Value;
    }

    public static implicit operator CoreId(string value)
    {
        return new CoreId(value);
    }

    public static implicit operator CoreId(int value)
    {
        return new CoreId(value.ToString());
    }

    public static implicit operator CoreId(long value)
    {
        return new CoreId(value.ToString());
    }
}