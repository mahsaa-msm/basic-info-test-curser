using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.Cities.Commands.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.ApplicationService.Cities.Commands.Update;

public class UpdateCityHandler : CommandHandler<UpdateCityCommand>
{
    private readonly ICityCommandRepository _cityCommandRepository;
    private readonly Dictionary<MoveDirection, Func<City, UpdateCityCommand, Task>> _actions;

    public UpdateCityHandler(ZaminServices zaminServices,
                             ICityCommandRepository cityCommandRepository)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (city, command) => (await _cityCommandRepository.GetSuperiorCities(city.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (city, command) => (await _cityCommandRepository.GetSubordinateCities(city.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateCityCommand command)
    {
        City city = await _cityCommandRepository.GetAsync(command.CityId);

        EntityGuard.ThrowIfNullWithLongId(city, ProjectTranslation.CITY);

        if (!_cityCommandRepository.IsCreatedByCore(city))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = city.Title.Value;
            command.Code = city.Code.Value;
        }

        await CheckPriority(command);

        await MoveCitiesIfNeeded(city, command);

        city.Update(command.ToParameter());

        await _cityCommandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateCityCommand command)
    {
        var nextPriority = await _cityCommandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveCitiesIfNeeded(City current, UpdateCityCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateCityCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _cityCommandRepository.ExistsAsync(c => c.Id != command.CityId &&
                                                     (Code.FromString(command.Code).Equals(c.Code) || DIPTitle.FromString(command.Title).Equals(c.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.CITY]);
    }
    #endregion
}
