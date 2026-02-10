using Master.Data.Core.Domain.IssuanceSchemes.Entities;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.IssuanceSchemes.Configs;

public sealed class IssuanceSchemeConfig : IEntityTypeConfiguration<IssuanceScheme>
{
    public void Configure(EntityTypeBuilder<IssuanceScheme> builder)
    {
        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.CoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.InsuranceTypeCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.DisplayTitle).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Code).HasMaxLength(ProjectConsts.CODE_MAX_LENGTH).IsRequired();

        builder.HasIndex(c => c.BusinessId).IsUnique();

        builder.HasIndex(c => c.CoreId);
        builder.HasIndex(c => c.InsuranceTypeCoreId);
        builder.HasIndex(c => new { c.TenantId, c.CoreId }).IsUnique();
    }
}