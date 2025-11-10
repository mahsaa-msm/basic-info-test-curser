using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.Cities.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Commands.Create;

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
            .ExistsAsync(e => e.Title == Title.FromString(command.Title) ||
                              e.Code == Code.FromString(command.Code) ||
                              e.CoreId == CoreId.FromString(command.CoreId));

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