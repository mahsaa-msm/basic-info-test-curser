using Master.Data.Core.Domain.Tenants.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Tenants.Conversions;

public sealed class TenantSlugConversion : ValueConverter<TenantSlug, string>
{
    public TenantSlugConversion() : base(slug => slug.Value, value => TenantSlug.FromString(value)) { }
}