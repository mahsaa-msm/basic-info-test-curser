using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Commands.Update;

public class UpdateTravelPassengerCountTypeCommandHandler : CommandHandler<UpdateTravelPassengerCountTypeCommand>
{
    private readonly ITravelPassengerCountTypeCommandRepository _travelPassengerCountTypeCommandRepository;

    public UpdateTravelPassengerCountTypeCommandHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeCommandRepository travelPassengerCountTypeCommandRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeCommandRepository = travelPassengerCountTypeCommandRepository;
    }
    public async override Task<CommandResult> Handle(UpdateTravelPassengerCountTypeCommand command)
    {
        var travelPassengerCountType = await _travelPassengerCountTypeCommandRepository.GetAsync(command.Id);
        EntityGuard.ThrowIfNullWithLongId(travelPassengerCountType, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);

        if (await _travelPassengerCountTypeCommandRepository
           .ExistsAsync(e => e.Title.Equals(Title.FromString(command.Title)) && e.Id != command.Id))
        {
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                 ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);
        }

        await UpdatePriorityForOtherRecords(travelPassengerCountType.Priority.Value, command.Priority, command.Id);
        travelPassengerCountType.Update(command.CoreId, command.Title, command.Priority);
        await _travelPassengerCountTypeCommandRepository.CommitAsync();

        return await OkAsync();
    }
    private async Task UpdatePriorityForOtherRecords(long previousOrder, long newOrder, int recordId)
    {
        var records = await _travelPassengerCountTypeCommandRepository.GetAllAsync();

        var recordsToUpdate = records
            .Where(e => e.Id != recordId && (
                previousOrder < newOrder && e.Priority.Value > previousOrder && e.Priority.Value <= newOrder ||
                previousOrder > newOrder && e.Priority.Value < previousOrder && e.Priority.Value >= newOrder
            ))
            .ToList();

        foreach (var record in recordsToUpdate)
        {
            var updatedOrder = record.Priority.Value + (previousOrder < newOrder ? -1 : 1);
            record.Update(record.CoreId, record.Title, updatedOrder);
        }
    }
}
