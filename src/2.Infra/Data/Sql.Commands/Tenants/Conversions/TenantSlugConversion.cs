using Vehicle.Insurance.Core.Domain.Tenants.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Tenants.Conversions;

public sealed class TenantSlugConversion : ValueConverter<TenantSlug, string>
{
    public TenantSlugConversion() : base(slug => slug.Value, value => TenantSlug.FromString(value)) { }
}
