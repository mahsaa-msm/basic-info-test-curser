using Vehicle.Insurance.Core.Domain.ParrotTranslations.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.ParrotTranslations.Commands;

public interface IParrotTranslationCommandRepository : ICommandRepository<ParrotTranslation, long>
{
}
