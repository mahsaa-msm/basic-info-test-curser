using Vehicle.Insurance.Infra.Data.Sql.Queries.AgreementObligations.Conversions;
using Vehicle.Insurance.Infra.Data.Sql.Queries.AgreementObligations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.AgreementObligations.Configs;

public sealed class AgreementObligationConfig : IEntityTypeConfiguration<AgreementObligation>
{
    public void Configure(EntityTypeBuilder<AgreementObligation> builder)
    {
        builder.Property(c => c.IssuanceSchemeCoreIds).HasConversion<IssuanceSchemeCoreIdsConversion>();

        builder.HasAlternateKey(c => new { c.TenantId, c.InsuranceTypeCoreId });

        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);

        builder
        .HasOne(c => c.InsuranceType)
        .WithMany()
        .HasPrincipalKey(c => new { c.TenantId, c.CoreId }) // کلید اصلی ترکیبی
        .HasForeignKey(p => new { p.TenantId, p.InsuranceTypeCoreId }) // کلید خارجی ترکیبی
        .IsRequired(false)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
