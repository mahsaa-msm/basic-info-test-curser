using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.ServiceFeatures.Configs;

public sealed class ServiceFetureConfig : IEntityTypeConfiguration<ServiceFeature>
{
    public void Configure(EntityTypeBuilder<ServiceFeature> builder)
    {
        builder.Property(c => c.ServiceName).HasMaxLength(ProjectConsts.NAME_MAX_LENGTH).IsRequired();

        builder.Property(c => c.FeatureName).HasMaxLength(ProjectConsts.NAME_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Description).HasMaxLength(ProjectConsts.DESCRIPTION_MAX_LENGTH).IsRequired(false);

        builder.Property(c => c.InsuranceTypeCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired(false);

        builder.HasIndex(c => c.BusinessId).IsUnique();


        builder.HasIndex(c => c.Key);
        builder.HasIndex(c => new { c.TenantId, c.Key }).IsUnique();
    }
}