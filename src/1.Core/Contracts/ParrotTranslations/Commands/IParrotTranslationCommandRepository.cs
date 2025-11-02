using Master.Data.Core.Domain.ParrotTranslations.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.ParrotTranslations.Commands;

public interface IParrotTranslationCommandRepository : ICommandRepository<ParrotTranslation, long>
{
}