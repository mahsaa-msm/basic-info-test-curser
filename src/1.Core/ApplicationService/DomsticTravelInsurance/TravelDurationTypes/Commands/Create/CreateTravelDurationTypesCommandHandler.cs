using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Create;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelDurationTypess.Commands.Create;

public class CreateTravelDurationTypesCommandHandler : CommandHandler<CreateTravelDurationTypesCommand, Guid>
{
    private readonly ITravelDurationTypesCommandRepository _TravelDurationTypesCommandRepository;

    public CreateTravelDurationTypesCommandHandler(ZaminServices zaminServices, ITravelDurationTypesCommandRepository
        TravelDurationTypesCommandRepository) : base(zaminServices)
    {
        _TravelDurationTypesCommandRepository = TravelDurationTypesCommandRepository;
    }
    public async override Task<CommandResult<Guid>> Handle(CreateTravelDurationTypesCommand command)
    {
        int maxOrder = await _TravelDurationTypesCommandRepository.GetMaxPriorityAsync();

        TravelDurationType TravelDurationTypes = TravelDurationType.Create(command.CoreId, command.Title, maxOrder + 1);

        await _TravelDurationTypesCommandRepository.InsertAsync(TravelDurationTypes);
        await _TravelDurationTypesCommandRepository.CommitAsync();

        return Ok(TravelDurationTypes.BusinessId.Value);
    }
}


