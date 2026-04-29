using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Tenants.Configs;

public sealed class TenantConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(ProjectConsts.NAME_MAX_LENGTH);
        builder.Property(c => c.Slug).HasMaxLength(ProjectConsts.TENANT_SLUG_MAX_LENGTH).IsRequired();

        builder
        .HasMany(c => c.Configs)
        .WithOne()
        .HasPrincipalKey(c => c.Id)
        .HasForeignKey(k => k.TenantId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.Slug).IsUnique();
    }
}
