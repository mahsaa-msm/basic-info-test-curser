using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Disable;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Commands.Disable;

public class DisableTravelPassengerCountTypeCommandHandler : CommandHandler<DisableTravelPassengerCountTypeCommand>
{
    private readonly ITravelPassengerCountTypeCommandRepository _travelPassengerCountTypeCommandRepository;

    public DisableTravelPassengerCountTypeCommandHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeCommandRepository travelPassengerCountTypeCommandRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeCommandRepository = travelPassengerCountTypeCommandRepository;
    }
    public async override Task<CommandResult> Handle(DisableTravelPassengerCountTypeCommand command)
    {
        var travelPassengerCountType = await _travelPassengerCountTypeCommandRepository.GetAsync(command.Id);
        EntityGuard.ThrowIfNullWithLongId(travelPassengerCountType, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);
        travelPassengerCountType.Disable();
        await _travelPassengerCountTypeCommandRepository.CommitAsync();

        return await OkAsync();
    }
}
