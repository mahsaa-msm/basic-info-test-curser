using Master.Data.Core.Contracts.ParrotTranslations.Commands;
using Master.Data.Core.Domain.ParrotTranslations.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.ParrotTranslations;

public sealed class ParrotTranslationCommandRepository : BaseCommandRepository<ParrotTranslation, MasterDataCommandDbContext, long>,
    IParrotTranslationCommandRepository
{
    public ParrotTranslationCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }
}
