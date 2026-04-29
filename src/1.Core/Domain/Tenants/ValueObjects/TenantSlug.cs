using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Resources;
using System.Text.RegularExpressions;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Tenants.ValueObjects;

public sealed class TenantSlug : BaseValueObject<TenantSlug>
{
    public string Value { get; private set; }

    public static TenantSlug FromString(string value) => new(value);

    private TenantSlug(string value)
    {
        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(value, ProjectTranslation.TENANT_SLUG);

        var normalized = value?.Trim().ToLowerInvariant();

        ValueObjectGuard.ThrowIfStringNullOrWhiteSpace(normalized, ProjectTranslation.TENANT_SLUG);

        ValueObjectGuard.ThrowIfIsNotBetween(normalized.Length,
                                             ProjectConsts.TENANT_SLUG_MIN_LENGTH,
                                             ProjectConsts.TENANT_SLUG_MAX_LENGTH,
                                             ProjectTranslation.TENANT_SLUG);

        ValueObjectGuard.ThrowIfNotValid(Regex.IsMatch(normalized, ProjectConsts.TENANT_SLUG_VALIDATION_PATTERN),
                                         ProjectTranslation.TENANT_SLUG);

        Value = normalized;
    }

    private TenantSlug()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator string(TenantSlug slug) => slug.Value;

    public static implicit operator TenantSlug(string value) => new(value);
    public override string ToString() => Value;
}

