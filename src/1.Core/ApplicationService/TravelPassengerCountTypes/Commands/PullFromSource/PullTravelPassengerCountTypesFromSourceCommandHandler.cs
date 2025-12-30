using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.PullFromSource;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using Zamin.Utilities.Extensions;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Commands.PullFromSource;

public class PullTravelPassengerCountTypesFromSourceCommandHandler : CommandHandler<PullTravelPassengerCountTypesFromSourceCommand, string>
{
    private readonly ITravelPassengerCountTypeCommandRepository _travelPassengerCountTypeCommandRepository;
    private readonly IPullTravelPassengerCountTypeDataFromCore _pullTravelPassengerCountTypeDataFromCore;

    public PullTravelPassengerCountTypesFromSourceCommandHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeCommandRepository travelPassengerCountTypeCommandRepository, IPullTravelPassengerCountTypeDataFromCore pullTravelPassengerCountTypeDataFromCore) : base(zaminServices)
    {
        _travelPassengerCountTypeCommandRepository = travelPassengerCountTypeCommandRepository;
        _pullTravelPassengerCountTypeDataFromCore = pullTravelPassengerCountTypeDataFromCore;
    }
    public async override Task<CommandResult<string>> Handle(PullTravelPassengerCountTypesFromSourceCommand command)
    {
        var source = await _pullTravelPassengerCountTypeDataFromCore.ExecuteAsync();
        var target = await _travelPassengerCountTypeCommandRepository.GetAllAsync();
        int maxOrder = await _travelPassengerCountTypeCommandRepository.GetMaxPriorityAsync();

        var addedTravelPassengerCountTypes = source
            .Where(x => !target.Any(c => c.CoreId == x.Id))
            .Select(item => new TravelPassengerCountType(item.Id, item.Title.ApplyCorrectYeKe(), maxOrder++))
            .ToList();

        if (addedTravelPassengerCountTypes.Any())
        {
            await _travelPassengerCountTypeCommandRepository.InsertRangeAsync(addedTravelPassengerCountTypes);
            await _travelPassengerCountTypeCommandRepository.CommitAsync();
        }

        string message = addedTravelPassengerCountTypes.Count == 0 ? ProjectValidationError.NO_NEW_INFORMATION_WAS_FOUND_TO_UPDATE :
             ProjectTranslation.UPDATE_DONE;
        return Ok(message);
    }
}
