using Master.Data.Infra.Data.Sql.Queries.Cities.Entities;
using Master.Data.Infra.Data.Sql.Queries.InsuranceUnits.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceUnits.Configs;

public sealed class InsuranceUnitConfig : IEntityTypeConfiguration<InsuranceUnit>
{
    public void Configure(EntityTypeBuilder<InsuranceUnit> builder)
    {
        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);

        builder
        .HasOne<City>()
        .WithMany()
        .HasPrincipalKey(c => new { c.TenantId, c.CoreId }) // کلید اصلی ترکیبی
        .HasForeignKey(p => new { p.TenantId, p.CityCoreId }); // کلید خارجی ترکیبی
    }
}