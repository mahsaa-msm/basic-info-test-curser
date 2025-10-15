using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Disable;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelDurationTypess.Commands.Disable;

public class DisableTravelDurationTypesCommandHandler : CommandHandler<DisableTravelDurationTypesCommand>
{
    private readonly ITravelDurationTypesCommandRepository _TravelDurationTypesCommandRepository;

    public DisableTravelDurationTypesCommandHandler(ZaminServices zaminServices,
        ITravelDurationTypesCommandRepository TravelDurationTypesCommandRepository) : base(zaminServices)
    {
        _TravelDurationTypesCommandRepository = TravelDurationTypesCommandRepository;
    }
    public async override Task<CommandResult> Handle(DisableTravelDurationTypesCommand command)
    {
        var TravelDurationTypes = await _TravelDurationTypesCommandRepository.GetAsync(command.Id);
        EntityGuard.ThrowIfNullWithLongId(TravelDurationTypes, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);
        TravelDurationTypes.Disable();
        await _TravelDurationTypesCommandRepository.CommitAsync();

        return await OkAsync();
    }
}
