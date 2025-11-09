using Master.Data.Infra.Data.Sql.Queries.Cities.Entities;
using Master.Data.Infra.Data.Sql.Queries.Provinces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.Cities.Configs;
public sealed class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);

        builder
        .HasOne<Province>()
        .WithMany()
        .HasPrincipalKey(c => new { c.TenantId, c.CoreId }) // کلید اصلی ترکیبی
        .HasForeignKey(p => new { p.TenantId, p.ProvinceCoreId }); // کلید خارجی ترکیبی
    }
}