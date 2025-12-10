using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Resources;
using System.Text.RegularExpressions;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.PatternCatalogs.ValueObjects;

public sealed class PatternKey : BaseValueObject<PatternKey>
{
    public string Value { get; }

    public PatternKey(string value)
    {
        ValueObjectGuard.ThrowIfNull(value, ProjectTranslation.PATTERN_KEY);

        ValueObjectGuard.ThrowIfStringLenghtIsNotBetween(value,
                                                         ProjectConsts.PATTERN_KEY_MIN_LENGTH,
                                                         ProjectConsts.PATTERN_KEY_MAX_LENGTH,
                                                         ProjectTranslation.PATTERN_KEY);

        ValueObjectGuard.ThrowIfNotValid(Regex.IsMatch(value,
                                                       string.Format(ProjectConsts.PATTERN_KEY_PATTERN,
                                                                     ProjectConsts.PATTERN_KEY_MIN_LENGTH,
                                                                     ProjectConsts.PATTERN_KEY_MAX_LENGTH)),
                                         ProjectTranslation.PATTERN_KEY);

        Value = value.ToLowerInvariant();
    }

    public static explicit operator string(PatternKey key) => key.Value;
    public static implicit operator PatternKey(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
    public static PatternKey FromString(string value) => new(value);
}
