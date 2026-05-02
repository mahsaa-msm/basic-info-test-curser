using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleTips;
using Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleTip.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Fetch;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Commands.Fetch;

public sealed class FetchMultiTenantVehicleBrandsFromSourceHandler
    : CommandHandler<FetchMultiTenantVehicleBrandsFromSourceCommand>
{
    private readonly ICoreInsuranceGetAllVehicleTipsCaller _caller;
    private readonly IVehicleBrandCommandRepository _commandRepository;

    public FetchMultiTenantVehicleBrandsFromSourceHandler(ZaminServices zaminServices,
        ICoreInsuranceGetAllVehicleTipsCaller caller,
        IVehicleBrandCommandRepository commandRepository) : base(zaminServices)
    {
        _caller = caller;
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantVehicleBrandsFromSourceCommand command)
    {
        var response = await _caller.Call(new GetAllVehicleTipsRequest());
        if (response.IsFailure)
            throw new InvalidOperationException(response.Error);

        var nextPriority = await _commandRepository.GetNextPriority();
        foreach (var item in response.Value!.DistinctBy(x => x.BrandID))
        {
            var rawTitle = string.IsNullOrWhiteSpace(item.Brand) ? item.BrandID.ToString() : item.Brand;
            var coreId = CoreId.FromString(item.BrandID.ToString());
            var existing = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(coreId);

            if (existing is null)
            {
                var created = VehicleBrand.Create(new CreateVehicleBrandParameter(
                    DIPTitle.FromString(rawTitle),
                    NullableTitle.FromString((string?)null),
                    coreId,
                    Priority.FromLong(nextPriority++)));
                await _commandRepository.InsertAsync(created);
                continue;
            }

            var title = DIPTitle.FromString(rawTitle);
            var updateParameter = new UpdateVehicleBrandParameter(title, DIPTitle.FromString(rawTitle),
                existing.Priority);
            existing.Update(updateParameter);
        }

        await _commandRepository.CommitAsync();
        return Ok();
    }
}
