using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.Cities.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Commands.ChangeActivation;

public class ChangeCitiesActivationHandler : CommandHandler<ChangeCitiesActivationCommand>
{
    private readonly ICityCommandRepository _cityCommandRepository;
    private static readonly Dictionary<bool, Action<List<City>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };

    public ChangeCitiesActivationHandler(ZaminServices zaminServices,
                                         ICityCommandRepository cityCommandRepository)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeCitiesActivationCommand command)
    {
        List<City> cities = await _cityCommandRepository.GetByIds(command.CitiesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(cities, ProjectTranslation.CITY);

        _actions[command.IsActive](cities);
        await _cityCommandRepository.CommitAsync();

        return Ok();

    }
}
