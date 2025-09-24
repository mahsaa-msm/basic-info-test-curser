using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelDurationTypess.Commands.Update;

public class UpdateTravelDurationTypesCommandHandler : CommandHandler<UpdateTravelDurationTypesCommand>
{
    private readonly ITravelDurationTypesCommandRepository _TravelDurationTypesCommandRepository;

    public UpdateTravelDurationTypesCommandHandler(ZaminServices zaminServices,
        ITravelDurationTypesCommandRepository TravelDurationTypesCommandRepository) : base(zaminServices)
    {
        _TravelDurationTypesCommandRepository = TravelDurationTypesCommandRepository;
    }
    public async override Task<CommandResult> Handle(UpdateTravelDurationTypesCommand command)
    {
        var TravelDurationTypes = await _TravelDurationTypesCommandRepository.GetAsync(command.Id);
        EntityGuard.ThrowIfNullWithLongId(TravelDurationTypes, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);

        if (await _TravelDurationTypesCommandRepository
           .ExistsAsync(e => e.Title.Equals(Title.FromString(command.Title)) && e.Id != command.Id))
        {
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                 ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE);
        }

        await UpdatePriorityForOtherRecords(TravelDurationTypes.Priority.Value, command.Priority, command.Id);
        TravelDurationTypes.Update(command.CoreId, command.Title, command.Priority);
        await _TravelDurationTypesCommandRepository.CommitAsync();

        return await OkAsync();
    }
    private async Task UpdatePriorityForOtherRecords(long previousOrder, long newOrder, int recordId)
    {
        var records = await _TravelDurationTypesCommandRepository.GetAllAsync();

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
