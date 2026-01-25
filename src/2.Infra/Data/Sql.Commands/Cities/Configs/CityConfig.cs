using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.Cities.Configs;

public sealed class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasQueryFilter(c => c.IsDeleted == IsDeleted.False());

        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.CoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.DisplayTitle).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Code).HasMaxLength(ProjectConsts.CODE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.ProvinceCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.HasIndex(c => c.BusinessId).IsUnique();


        builder.HasIndex(c => c.CoreId);
        builder.HasIndex(c => c.ProvinceCoreId);
        builder.HasIndex(c => new { c.TenantId, c.CoreId }).IsUnique();
    }
}