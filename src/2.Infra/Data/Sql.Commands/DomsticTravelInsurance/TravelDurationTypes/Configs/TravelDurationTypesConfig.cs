using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Commands.TravelDurationTypes.Configs;

public class TravelDurationTypesConfig : IEntityTypeConfiguration<TravelDurationType>
{
    public void Configure(EntityTypeBuilder<TravelDurationType> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();
        builder.Property(x => x.IsEnable).HasDefaultValue(true);
    }
}
