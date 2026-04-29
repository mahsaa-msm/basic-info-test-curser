using Vehicle.Insurance.Core.Contracts.Countries.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Countries.Entities;
using Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Countries.Commands.Create;

public class CreateCountryHandler : CommandHandler<CreateCountryCommand, long>
{
    private readonly ICountryCommandRepository _commandRepository;
    private readonly Dictionary<bool, Func<CreateCountryCommand, long, Country, Task<Country>>> _actions;

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
            .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                              Code.FromString(command.Code).Equals(e.Code) ||
                              CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateCountry)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        Country? country = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _commandRepository.GetNextPriority();

        country = await _actions[country is null](command, nextPriority, country);

        await _commandRepository.CommitAsync();

        return Ok(country.Id);
    }

    #region Methods
    private async Task<Country> Create(CreateCountryCommand command, long nextPriority, Country? country)
    {
        country = Country.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(country);

        return country;
    }

    private async Task<Country> Restore(CreateCountryCommand command, long nextPriority, Country? country)
    {
        country?.Restore(command.ToRestoreParameter(nextPriority));

        return country;
    }
    #endregion
}
