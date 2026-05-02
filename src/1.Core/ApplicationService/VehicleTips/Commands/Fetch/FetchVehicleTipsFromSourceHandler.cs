using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleTips;
using Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleTip.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Fetch;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Commands.Fetch;

public sealed class FetchVehicleTipsFromSourceHandler : CommandHandler<FetchVehicleTipsFromSourceCommand>
{
    private readonly ICoreInsuranceGetAllVehicleTipsCaller _caller;
    private readonly IVehicleTipCommandRepository _commandRepository;

    public FetchVehicleTipsFromSourceHandler(ZaminServices zaminServices,
        ICoreInsuranceGetAllVehicleTipsCaller caller,
        IVehicleTipCommandRepository commandRepository) : base(zaminServices)
    {
        _caller = caller;
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(FetchVehicleTipsFromSourceCommand command)
    {
        var response = await _caller.Call(new GetAllVehicleTipsRequest());
        if (response.IsFailure)
            throw new InvalidOperationException(response.Error);

        var nextPriority = await _commandRepository.GetNextPriority();

        foreach (var item in response.Value!)
        {
            var rawTitle = string.IsNullOrWhiteSpace(item.Tip) ? item.TipID.ToString() : item.Tip;
            var coreId = CoreId.FromString(item.TipID.ToString());
            var existing = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(coreId);

            var brandCoreId = CoreId.FromString(item.BrandID.ToString());
            var vehicleTypeCoreId = CoreId.FromString(item.NoeVasilehID.ToString());
            var vehicleSystemCoreId = CoreId.FromString(item.SystemID.ToString());

            if (existing is null)
            {
                var created = VehicleTip.Create(new CreateVehicleTipParameter(
                    DIPTitle.FromString(rawTitle),
                    NullableTitle.FromString((string?)null),
                    coreId,
                    brandCoreId,
                    vehicleTypeCoreId,
                    vehicleSystemCoreId,
                    Priority.FromLong(nextPriority++)));
                await _commandRepository.InsertAsync(created);
                continue;
            }

            var title = DIPTitle.FromString(rawTitle);
            var updateParameter = new UpdateVehicleTipParameter(
                title,
                DIPTitle.FromString(rawTitle),
                brandCoreId,
                vehicleTypeCoreId,
                vehicleSystemCoreId,
                existing.Priority);
            existing.Update(updateParameter);
        }

        await _commandRepository.CommitAsync();
        return Ok();
    }
}
