using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Common.ValueObjects;

public class DIPTitle : BaseValueObject<DIPTitle>
{
    public string Value { get; private set; }

    public static DIPTitle FromString(string value) => new(value);

    private DIPTitle(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.TITLE);

        ValueObjectGuard.ThrowIfIsNotBetween(value.Length,
                                             ProjectConsts.TITLE_MIN_LENGTH,
                                             ProjectConsts.TITLE_MAX_LENGTH,
                                             ProjectTranslation.TITLE);

        Value = value;
    }

    private DIPTitle()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator string(DIPTitle title) => title.Value;

    public static implicit operator DIPTitle(string value) => new(value);
    public override string ToString() => Value;

}