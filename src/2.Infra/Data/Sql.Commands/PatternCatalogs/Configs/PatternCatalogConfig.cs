using Master.Data.Core.Domain.PatternCatalogs.Entities;
using Master.Data.Core.Resources;
using Master.Data.Infra.Data.Sql.Commands.PatternCatalogs.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.PatternCatalogs.Configs;

public sealed class PatternCatalogConfig : IEntityTypeConfiguration<PatternCatalog>
{
    public void Configure(EntityTypeBuilder<PatternCatalog> builder)
    {
        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.Key).HasMaxLength(ProjectConsts.PATTERN_KEY_MAX_LENGTH).HasConversion<PatternKeyConversion>().IsRequired();

        builder.Property(c => c.Pattern).HasMaxLength(ProjectConsts.PATTERN_MAX_LENGTH).HasConversion<RegexExpressionConversion>().IsRequired();

        builder.Property(c => c.Description).HasMaxLength(ProjectConsts.DESCRIPTION_MAX_LENGTH).IsRequired(false);


        builder.HasIndex(c => c.BusinessId).IsUnique();
        builder.HasIndex(c => c.Key);
        builder.HasIndex(c => new { c.TenantId, c.Key }).IsUnique();
        builder.HasIndex(c => new { c.TenantId, c.Id }).IsUnique();
        builder.HasIndex(c => new { c.TenantId, c.Key, c.Type }).IsUnique();
    }
}