using Vehicle.Insurance.Infra.Data.Sql.Queries.IssuanceSchemes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.IssuanceSchemes.Configs;
public sealed class IssuanceSchemeConfig : IEntityTypeConfiguration<IssuanceScheme>
{
    public void Configure(EntityTypeBuilder<IssuanceScheme> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });

        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);
    }
}
