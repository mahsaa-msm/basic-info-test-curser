using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.Entities;
using Master.Data.Core.Resources;
using Master.Data.Infra.Data.Sql.Commands.InsuranceUnits.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.InsuranceUnits.Configs;

public sealed class InsuranceUnitConfig : IEntityTypeConfiguration<InsuranceUnit>
{
    public void Configure(EntityTypeBuilder<InsuranceUnit> builder)
    {
        builder.HasQueryFilter(c => c.IsDeleted == IsDeleted.False());

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.CoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Name).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.DisplayTitle).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Code).HasMaxLength(ProjectConsts.CODE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.CityCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(x => x.Location)
            .HasConversion<GeoCoordinateConversion>()
            .HasColumnName("Location")
            .HasColumnType("geometry")
            .IsRequired(false);

        builder.HasIndex(c => c.BusinessId).IsUnique();

        builder.HasIndex(c => c.CoreId);
        builder.HasIndex(c => c.CityCoreId);
        builder.HasIndex(c => new { c.TenantId, c.CoreId }).IsUnique();

        // ایجاد Spatial Index برای عملکرد بهینه
        builder.HasIndex(x => x.Location)
            .HasDatabaseName("IX_InsuranceUnits_Location_Spatial")
            .HasAnnotation("SqlServer:IndexType", "SPATIAL");
    }
}