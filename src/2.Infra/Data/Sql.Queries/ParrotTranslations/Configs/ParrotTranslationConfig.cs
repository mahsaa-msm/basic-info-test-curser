using Agent.Management.Infra.Data.Sql.Queries.ParrotTranslations.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agent.Management.Infra.Data.Sql.Queries.ParrotTranslations.Configs;

public sealed class ParrotTranslationConfig : IEntityTypeConfiguration<ParrotTranslation>
{
    public void Configure(EntityTypeBuilder<ParrotTranslation> builder)
    {
        builder.HasKey(k => k.Id);
    }
}
