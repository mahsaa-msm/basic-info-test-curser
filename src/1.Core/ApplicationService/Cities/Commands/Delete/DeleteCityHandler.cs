using Vehicle.Insurance.Core.Contracts.Cities.Commands;
using Vehicle.Insurance.Core.Domain.Cities.Entities;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Cities.Commands.Delete;

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
