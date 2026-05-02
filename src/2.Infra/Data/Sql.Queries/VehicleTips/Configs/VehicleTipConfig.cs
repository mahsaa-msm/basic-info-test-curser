using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleTips.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleTips.Configs;

public sealed class VehicleTipConfig : IEntityTypeConfiguration<VehicleTip>
{
    public void Configure(EntityTypeBuilder<VehicleTip> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });
    }
}
