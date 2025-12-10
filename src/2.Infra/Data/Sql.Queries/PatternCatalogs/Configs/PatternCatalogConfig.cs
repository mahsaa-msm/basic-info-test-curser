using Master.Data.Infra.Data.Sql.Queries.PatternCatalogs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.PatternCatalogs.Configs;

public sealed class PatternCatalogConfig : IEntityTypeConfiguration<PatternCatalog>
{
    public void Configure(EntityTypeBuilder<PatternCatalog> builder)
    {
        builder
        .HasOne(c => c.Tenant)
        .WithMany()
        .HasForeignKey(c => c.TenantId);
    }
}