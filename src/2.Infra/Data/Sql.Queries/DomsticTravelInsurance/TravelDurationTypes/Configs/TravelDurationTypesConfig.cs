using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.TravelDurationTypes.Configs;

public class TravelDurationTypesConfig : IEntityTypeConfiguration<TravelDurationType>
{
    public void Configure(EntityTypeBuilder<TravelDurationType> builder)
    {
    }
}
