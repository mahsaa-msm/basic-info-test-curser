using Master.Data.Core.Domain.ParrotTranslations.Entities;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.ParrotTranslations.Configs;

public sealed class ParrotTranslationConfig : IEntityTypeConfiguration<ParrotTranslation>
{
    public void Configure(EntityTypeBuilder<ParrotTranslation> builder)
    {
        builder.Property(x => x.Key).HasMaxLength(ProjectConsts.TRANSLATION_KEY_MAX_LENGTH).IsRequired();
        builder.Property(x => x.Value).HasMaxLength(ProjectConsts.TRANSLATION_VALUE_MAX_LENGTH).IsRequired();
        builder.Property(x => x.Culture).HasMaxLength(ProjectConsts.TRANSLATION_CULTURE_LENGTH);
    }
}
