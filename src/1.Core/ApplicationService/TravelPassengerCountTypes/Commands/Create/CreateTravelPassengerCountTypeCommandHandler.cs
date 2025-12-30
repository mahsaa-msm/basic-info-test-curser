using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Create;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Commands.Create;

public class CreateTravelPassengerCountTypeCommandHandler : CommandHandler<CreateTravelPassengerCountTypeCommand, Guid>
{
    private readonly ITravelPassengerCountTypeCommandRepository _travelPassengerCountTypeCommandRepository;

    public CreateTravelPassengerCountTypeCommandHandler(ZaminServices zaminServices, ITravelPassengerCountTypeCommandRepository
        travelPassengerCountTypeCommandRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeCommandRepository = travelPassengerCountTypeCommandRepository;
    }
    public async override Task<CommandResult<Guid>> Handle(CreateTravelPassengerCountTypeCommand command)
    {
        int maxOrder = await _travelPassengerCountTypeCommandRepository.GetMaxPriorityAsync();

        TravelPassengerCountType travelPassengerCountType = TravelPassengerCountType.Create(command.CoreId, command.Title, maxOrder + 1);

        await _travelPassengerCountTypeCommandRepository.InsertAsync(travelPassengerCountType);
        await _travelPassengerCountTypeCommandRepository.CommitAsync();

        return Ok(travelPassengerCountType.BusinessId.Value);
    }
}


