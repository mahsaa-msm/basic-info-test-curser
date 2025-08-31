using Master.Data.Core.Domain.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.Tenants.Configs;
public sealed class TenantConfigConfig : IEntityTypeConfiguration<Core.Domain.Tenants.Entities.TenantConfig>
{
    public void Configure(EntityTypeBuilder<Core.Domain.Tenants.Entities.TenantConfig> builder)
    {
        // تنظیمات مربوط به ذخیره JSON (فیلد خصوصی)
        builder.Property("_settingsJson")
            .HasColumnName("SettingsJson")
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        // ignore properties that shouldn't be mapped
        builder.Ignore(tc => tc.Settings);

        builder.HasOne<Tenant>()
            .WithMany(t => t.Configs)
            .HasForeignKey(tc => tc.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.TenantConfigSettingHistories)
            .WithOne()
            .HasPrincipalKey(c => c.Id)
            .HasForeignKey(k => k.TenantConfigId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(tc => new { tc.TenantId, tc.ConfigType })
            .IsUnique();
    }
}