using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants.Configs;
public sealed class TenantConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder
        .HasMany(c => c.Configs)
        .WithOne(k => k.Tenant)
        .HasPrincipalKey(c => c.Id)
        .HasForeignKey(k => k.TenantId);
    }
}