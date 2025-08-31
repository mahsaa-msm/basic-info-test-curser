using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.RequestResponse.Countries.Commands.Delete;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Commands.Delete;

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