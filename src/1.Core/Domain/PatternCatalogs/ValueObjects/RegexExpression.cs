using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using System.Text.RegularExpressions;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;

public sealed class RegexExpression : BaseValueObject<RegexExpression>
{
    public string Value { get; }

    public RegexExpression(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.REGEX_EXPRESSION);

        ValueObjectGuard.ThrowIfStringLenghtGreaterThan(value, ProjectConsts.PATTERN_MAX_LENGTH, ProjectTranslation.REGEX_EXPRESSION);

        try
        {
            var regex = new Regex(value);
        }
        catch (ArgumentException ex)
        {
            ValueObjectGuard.ThrowIfNotValid(false, ProjectTranslation.REGEX_EXPRESSION);
        }

        Value = value;
    }

    public static explicit operator string(RegexExpression regexExpression)
    {
        return regexExpression.Value;
    }

    public static implicit operator RegexExpression(string value)
    {
        return new RegexExpression(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static RegexExpression FromString(string value) => new(value);
}

