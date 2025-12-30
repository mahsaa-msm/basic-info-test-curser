using Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes.Configs;

public class TravelPassengerCountTypeConfig : IEntityTypeConfiguration<TravelPassengerCountType>
{
    public void Configure(EntityTypeBuilder<TravelPassengerCountType> builder)
    {
    }
}
