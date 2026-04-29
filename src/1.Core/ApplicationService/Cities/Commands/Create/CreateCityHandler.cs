using Vehicle.Insurance.Core.Contracts.Cities.Commands;
using Vehicle.Insurance.Core.Domain.Cities.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Cities.Commands.Create;

public class CreateCityHandler : CommandHandler<CreateCityCommand, long>
{
    private readonly ICityCommandRepository _cityCommandRepository;
    private readonly Dictionary<bool, Func<CreateCityCommand, long, City, Task<City>>> _actions;

    public CreateCityHandler(ZaminServices zaminServices,
                                ICityCommandRepository cityCommandRepository)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
        _actions = new()
        {
            [true] = async (command, nextPriority, city) => await Create(command, nextPriority, city),
            [false] = async (command, nextPriority, city) => await Restore(command, nextPriority, city),
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateCityCommand command)
    {
        var isDuplicateCity = await _cityCommandRepository
            .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                              Code.FromString(command.Code).Equals(e.Code) ||
                              CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateCity)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        City? city = await _cityCommandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _cityCommandRepository.GetNextPriority();

        city = await _actions[city is null](command, nextPriority, city);

        await _cityCommandRepository.CommitAsync();

        return Ok(city.Id);
    }

    #region Methods
    private async Task<City> Create(CreateCityCommand command, long nextPriority, City? city)
    {
        city = City.Create(command.ToCreateParameter(nextPriority));

        await _cityCommandRepository.InsertAsync(city);

        return city;
    }

    private async Task<City> Restore(CreateCityCommand command, long nextPriority, City? city)
    {
        city?.Restore(command.ToRestoreParameter(nextPriority));

        return city;
    }
    #endregion
}
