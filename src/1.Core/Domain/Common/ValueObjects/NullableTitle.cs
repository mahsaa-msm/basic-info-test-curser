using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class NullableTitle : BaseValueObject<NullableTitle>
{
    public string? Value { get; private set; } = string.Empty;


    public bool IsNull { get; }

    private NullableTitle()
    {
    }

    public NullableTitle(string? value)
    {
        IsNull = string.IsNullOrWhiteSpace(value);
        if (!IsNull)
        {
            value = value?.RemoveExcessWhiteSpace();
            ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                             ProjectConsts.TITLE_MIN_LENGTH,
                                                             ProjectConsts.TITLE_MAX_LENGTH,
                                                             ProjectTranslation.TITLE);
            Value = value;
        }
    }

    public static NullableTitle FromString(string? value)
    {
        return value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string? ToString()
    {
        return Value?.ToString();
    }

    public static explicit operator string?(NullableTitle nullableTitle)
    {
        return nullableTitle.Value;
    }

    public static implicit operator NullableTitle(string? value)
    {
        return new NullableTitle(value);
    }
}