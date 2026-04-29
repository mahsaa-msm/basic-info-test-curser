using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Commands;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.ParrotTranslations;

public sealed class ParrotTranslationCommandRepository : BaseCommandRepository<ParrotTranslation, VehicleInsuranceCommandDbContext, long>,
    IParrotTranslationCommandRepository
{
    public ParrotTranslationCommandRepository(VehicleInsuranceCommandDbContext dbContext) : base(dbContext)
    {
    }
}

