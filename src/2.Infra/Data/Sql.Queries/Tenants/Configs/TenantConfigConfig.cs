using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants.Configs;
public sealed class TenantConfigConfig : IEntityTypeConfiguration<Entities.TenantConfig>
{
    public void Configure(EntityTypeBuilder<Entities.TenantConfig> builder)
    {
        builder.HasOne(t=>t.Tenant)
            .WithMany(t => t.Configs)
            .HasForeignKey(tc => tc.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.TenantConfigSettingHistories)
            .WithOne()
            .HasPrincipalKey(c => c.Id)
            .HasForeignKey(k => k.TenantConfigId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}