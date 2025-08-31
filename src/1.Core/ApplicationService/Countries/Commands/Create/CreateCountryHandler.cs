using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.RequestResponse.Countries.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Commands.Create;

public class CreateCountryHandler : CommandHandler<CreateCountryCommand, long>
{
    private readonly ICountryCommandRepository _commandRepository;
    private readonly Dictionary<bool, Func<CreateCountryCommand, int, Country, Task<Country>>> _actions;

    public CreateCountryHandler(ZaminServices zaminServices,
                                ICountryCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [true] = async (command, nextPriority, country) => await Create(command, nextPriority, country),
            [false] = async (command, nextPriority, country) => await Restore(command, nextPriority, country),
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateCountryCommand command)
    {
        var isDuplicateCountry = await _commandRepository
            .ExistsAsync(e => e.Title == Title.FromString(command.Title) ||
                              e.Code == Code.FromString(command.Code) ||
                              e.CoreId == CoreId.FromString(command.CoreId));

        if (isDuplicateCountry)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        Country? country = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        int nextPriority = await _commandRepository.GetNextPriority();

        country = await _actions[country is null](command, nextPriority, country);

        await _commandRepository.CommitAsync();

        return Ok(country.Id);
    }

    #region Methods
    private async Task<Country> Create(CreateCountryCommand command, int nextPriority, Country? country)
    {
        country = Country.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(country);

        return country;
    }

    private async Task<Country> Restore(CreateCountryCommand command, int nextPriority, Country? country)
    {
        country?.Restore(command.ToRestoreParameter(nextPriority));

        return country;
    }
    #endregion
}