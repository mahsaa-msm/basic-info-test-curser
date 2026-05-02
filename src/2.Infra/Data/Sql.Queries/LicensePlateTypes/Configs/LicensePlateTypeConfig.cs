using Vehicle.Insurance.Infra.Data.Sql.Queries.LicensePlateTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.LicensePlateTypes.Configs;

public sealed class LicensePlateTypeConfig : IEntityTypeConfiguration<LicensePlateType>
{
    public void Configure(EntityTypeBuilder<LicensePlateType> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });
    }
}
