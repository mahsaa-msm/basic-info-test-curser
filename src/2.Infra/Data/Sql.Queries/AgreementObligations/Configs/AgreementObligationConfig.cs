using Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Conversions;
using Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Configs;

public sealed class AgreementObligationConfig : IEntityTypeConfiguration<AgreementObligation>
{
    public void Configure(EntityTypeBuilder<AgreementObligation> builder)
    {
        builder.Property(c => c.IssuanceSchemeCoreIds).HasConversion<IssuanceSchemeCoreIdsConversion>();

        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);
    }
}