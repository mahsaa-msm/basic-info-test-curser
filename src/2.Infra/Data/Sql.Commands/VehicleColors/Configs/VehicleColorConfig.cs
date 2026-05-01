using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.VehicleColors.Configs;

public sealed class VehicleColorConfig : IEntityTypeConfiguration<VehicleColor>
{
    public void Configure(EntityTypeBuilder<VehicleColor> builder)
    {
        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.CoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.DisplayTitle).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.ColorHash).HasMaxLength(ProjectConsts.COLOR_HASH_MAX_LENGTH).IsRequired();

        builder.HasIndex(c => c.BusinessId).IsUnique();

        builder.HasIndex(c => c.CoreId);
        builder.HasIndex(c => new { c.TenantId, c.CoreId }).IsUnique();
    }
}


