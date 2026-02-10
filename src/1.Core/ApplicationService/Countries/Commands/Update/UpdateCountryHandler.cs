using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.RequestResponse.Countries.Commands.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.ApplicationService.Countries.Commands.Update;

public class UpdateCountryHandler : CommandHandler<UpdateCountryCommand>
{
    private readonly ICountryCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<Country, UpdateCountryCommand, Task>> _actions;

    public UpdateCountryHandler(ZaminServices zaminServices,
                                ICountryCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (county, command) => (await _commandRepository.GetSuperiorCountries(county.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (county, command) => (await _commandRepository.GetSubordinateCountries(county.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateCountryCommand command)
    {
        Country country = await _commandRepository.GetAsync(command.CountryId);

        EntityGuard.ThrowIfNullWithLongId(country, ProjectTranslation.COUNTRY);

        if (!_commandRepository.IsCreatedByCore(country))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = country.Title.Value;
            command.Code = country.Code.Value;
        }

        await CheckPriority(command);

        await MoveCountriesIfNeeded(country, command);

        country.Update(command.ToParameter());

        await _commandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateCountryCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveCountriesIfNeeded(Country current, UpdateCountryCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateCountryCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.CountryId &&
                                                      (Code.FromString(command.Code).Equals(c.Code) || DIPTitle.FromString(command.Title).Equals(c.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.COUNTRY]);
    }
    #endregion
}
