using Vehicle.Insurance.Core.Contracts.Countries.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Countries.Entities;
using Vehicle.Insurance.Core.RequestResponse.Countries.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Countries.Commands.ChangeActivation;

public class ChangeCountriesActivationHandler : CommandHandler<ChangeCountriesActivationCommand>
{
    private readonly ICountryCommandRepository _commandRepository;
    private static readonly Dictionary<bool, Action<List<Country>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeCountriesActivationHandler(ZaminServices zaminServices,
                                          ICountryCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeCountriesActivationCommand command)
    {
        List<Country> countries = await _commandRepository.GetByIds(command.CountriesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(countries, ProjectTranslation.COUNTRY);

        _actions[command.IsActive](countries);
        await _commandRepository.CommitAsync();

        return Ok();

    }
}

