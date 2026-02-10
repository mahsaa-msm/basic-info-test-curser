using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.Tenants.Configs;

public sealed class TenantConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(ProjectConsts.NAME_MAX_LENGTH);

        builder
        .HasMany(c => c.Configs)
        .WithOne()
        .HasPrincipalKey(c => c.Id)
        .HasForeignKey(k => k.TenantId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.Name);
    }
}