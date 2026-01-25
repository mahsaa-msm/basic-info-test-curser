using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class Code : BaseValueObject<Code>
{
    public string Value { get; private set; }

    private Code()
    {
    }

    private Code(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.CODE);
        value = value.RemoveExcessWhiteSpace();
        ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                         ProjectConsts.CODE_MIN_LENGTH,
                                                         ProjectConsts.CODE_MAX_LENGTH,
                                                         ProjectTranslation.CODE);
        Value = value;
    }

    public static Code FromString(string value)
    {
        return new Code(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Code(string value)
    {
        return new Code(value);
    }

    public static explicit operator string(Code code)
    {
        return code.Value;
    }
}