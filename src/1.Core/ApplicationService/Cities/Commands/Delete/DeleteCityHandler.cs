using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.Cities.Commands.Delete;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Commands.Delete;

public class DeleteCityHandler : CommandHandler<DeleteCityCommand>
{
    private readonly ICityCommandRepository _cityCommandRepository;

    public DeleteCityHandler(ZaminServices zaminServices,
                             ICityCommandRepository cityCommandRepository)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteCityCommand command)
    {
        var city = await _cityCommandRepository.GetAsync(command.CityId);
        EntityGuard.ThrowIfNull<City, long>(city, ProjectTranslation.CITY);

        city.Delete();

        var subordinates = await _cityCommandRepository.GetSubordinateCities(city.Priority);

        subordinates?.ForEach(country => country.PullUp());

        await _cityCommandRepository.CommitAsync();

        return Ok();
    }
}