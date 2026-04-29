using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleColors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleColors.Configs;

public sealed class VehicleColorConfig : IEntityTypeConfiguration<VehicleColor>
{
    public void Configure(EntityTypeBuilder<VehicleColor> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });

        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);
    }
}

