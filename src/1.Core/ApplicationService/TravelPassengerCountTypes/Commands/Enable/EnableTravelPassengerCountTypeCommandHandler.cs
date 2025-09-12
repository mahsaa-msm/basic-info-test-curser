using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Enable;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Commands.Enable;

public class EnableTravelPassengerCountTypeCommandHandler : CommandHandler<EnableTravelPassengerCountTypeCommand>
{
    private readonly ITravelPassengerCountTypeCommandRepository _travelPassengerCountTypeCommandRepository;

    public EnableTravelPassengerCountTypeCommandHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeCommandRepository travelPassengerCountTypeCommandRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeCommandRepository = travelPassengerCountTypeCommandRepository;
    }
    public async override Task<CommandResult> Handle(EnableTravelPassengerCountTypeCommand command)
    {
        var travelPassengerCountType = await _travelPassengerCountTypeCommandRepository.GetAsync(command.Id);

        EntityGuard.ThrowIfNullWithLongId(travelPassengerCountType, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);
        travelPassengerCountType.Enable();

        await _travelPassengerCountTypeCommandRepository.CommitAsync();

        return await OkAsync();
    }
}
