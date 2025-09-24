using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Core.RequestResponse.TravelDurationTypess.PullFromSource;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using Zamin.Utilities.Extensions;

namespace Master.Data.Core.ApplicationService.TravelDurationTypess.Commands.PullFromSource;

public class PullTravelDurationTypessFromSourceCommandHandler : CommandHandler<PullTravelDurationTypessFromSourceCommand, string>
{
    private readonly ITravelDurationTypesCommandRepository _TravelDurationTypesCommandRepository;
    private readonly IPullTravelDurationTypesDataFromCore _pullTravelDurationTypesDataFromCore;

    public PullTravelDurationTypessFromSourceCommandHandler(ZaminServices zaminServices,
        ITravelDurationTypesCommandRepository TravelDurationTypesCommandRepository, IPullTravelDurationTypesDataFromCore pullTravelDurationTypesDataFromCore) : base(zaminServices)
    {
        _TravelDurationTypesCommandRepository = TravelDurationTypesCommandRepository;
        _pullTravelDurationTypesDataFromCore = pullTravelDurationTypesDataFromCore;
    }
    public async override Task<CommandResult<string>> Handle(PullTravelDurationTypessFromSourceCommand command)
    {
        var source = await _pullTravelDurationTypesDataFromCore.ExecuteAsync();
        var target = await _TravelDurationTypesCommandRepository.GetAllAsync();
        int maxOrder = await _TravelDurationTypesCommandRepository.GetMaxPriorityAsync();

        List<TravelDurationType> addedTravelDurationTypes = source
            .Where(x => !target.Any(c => c.CoreId == x.Id))
            .Select(item => new TravelDurationType(item.Id, item.Title.ApplyCorrectYeKe(), maxOrder++))
            .ToList();

        if (addedTravelDurationTypes.Any())
        {
            await _TravelDurationTypesCommandRepository.InsertRangeAsync(addedTravelDurationTypes);
            await _TravelDurationTypesCommandRepository.CommitAsync();
        }

        string message = addedTravelDurationTypes.Count == 0 ? ProjectValidationError.NO_NEW_INFORMATION_WAS_FOUND_TO_UPDATE :
             ProjectTranslation.UPDATE_DONE;
        return Ok(message);
    }
}
