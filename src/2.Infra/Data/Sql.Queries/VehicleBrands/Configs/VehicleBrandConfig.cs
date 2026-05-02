using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleBrands.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleBrands.Configs;

public sealed class VehicleBrandConfig : IEntityTypeConfiguration<VehicleBrand>
{
    public void Configure(EntityTypeBuilder<VehicleBrand> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });
    }
}
