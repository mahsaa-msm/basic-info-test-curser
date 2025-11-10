using Master.Data.Infra.Data.Sql.Queries.Countries.Entities;
using Master.Data.Infra.Data.Sql.Queries.Provinces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.Provinces.Configs;
public sealed class ProvinceConfig : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);

        builder
        .HasOne<Country>()
        .WithMany()
        .HasPrincipalKey(c => new { c.TenantId, c.CoreId }) // کلید اصلی ترکیبی
        .HasForeignKey(p => new { p.TenantId, p.CountryCoreId }); // کلید خارجی ترکیبی
    }
}