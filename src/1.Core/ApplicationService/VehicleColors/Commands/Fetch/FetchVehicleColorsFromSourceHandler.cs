using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleColors;
using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleColor.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Fetch;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Commands.Fetch;

public sealed class FetchVehicleColorsFromSourceHandler : CommandHandler<FetchVehicleColorsFromSourceCommand>
{
    private readonly ICoreInsuranceGetAllVehicleColorsCaller _caller;
    private readonly IVehicleColorCommandRepository _commandRepository;

    public FetchVehicleColorsFromSourceHandler(ZaminServices zaminServices,
                                               ICoreInsuranceGetAllVehicleColorsCaller caller,
                                               IVehicleColorCommandRepository commandRepository) : base(zaminServices)
    {
        _caller = caller;
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(FetchVehicleColorsFromSourceCommand command)
    {
        var sourceDataResponse = await _caller.Call(new GetAllVehicleColorsRequest());

        if (sourceDataResponse.IsFailure)
            throw new InvalidOperationException(sourceDataResponse.Error);

        var nextPriority = await _commandRepository.GetNextPriority();

        foreach (var item in sourceDataResponse.Value)
        {
            var coreId = CoreId.FromString(item.rangBadanahID.ToString());
            var existing = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(coreId);

            if (existing is null)
            {
                var created = VehicleColor.Create(new CreateVehicleColorParameter(
                    DIPTitle.FromString(item.title),
                    NullableTitle.FromString(item.displayTitle),
                    coreId,
                    ColorHash.FromString(item.colorHash),
                    Priority.FromLong(nextPriority++)));

                await _commandRepository.InsertAsync(created);
                continue;
            }

            var updateParameter = new UpdateVehicleColorParameter(
                DIPTitle.FromString(item.title),
                DIPTitle.FromString(item.displayTitle ?? item.title),
                ColorHash.FromString(item.colorHash),
                existing.Priority);

            if (existing.IsDeleted.Value)
            {
                var restoreParameter = new RestoreVehicleColorParameter(
                    updateParameter.Title,
                    updateParameter.DisplayTitle.Value,
                    updateParameter.ColorHash,
                    updateParameter.Priority);
                existing.Restore(restoreParameter);
            }
            else
            {
                existing.Update(updateParameter);
            }
        }

        await _commandRepository.CommitAsync();
        return Ok();
    }
}

