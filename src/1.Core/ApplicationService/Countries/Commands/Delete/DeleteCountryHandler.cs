using Vehicle.Insurance.Core.Contracts.Countries.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Countries.Entities;
using Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Countries.Commands.Delete;

public class DeleteCountryHandler : CommandHandler<DeleteCountryCommand>
{
    private readonly ICountryCommandRepository _commandRepository;

    public DeleteCountryHandler(ZaminServices zaminServices,
                                ICountryCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteCountryCommand command)
    {
        var country = await _commandRepository.GetAsync(command.CountryId);
        EntityGuard.ThrowIfNull<Country, long>(country, ProjectTranslation.COUNTRY);

        country.Delete();

        var subordinates = await _commandRepository.GetSubordinateCountries(country.Priority);

        subordinates?.ForEach(country => country.PullUp());

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
