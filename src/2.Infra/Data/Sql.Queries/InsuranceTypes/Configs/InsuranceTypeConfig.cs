using Master.Data.Infra.Data.Sql.Queries.InsuranceTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceTypes.Configs;
public sealed class InsuranceTypeConfig : IEntityTypeConfiguration<InsuranceType>
{
    public void Configure(EntityTypeBuilder<InsuranceType> builder)
    {
        builder.HasAlternateKey(c => new { c.TenantId, c.CoreId });

        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);
    }
}