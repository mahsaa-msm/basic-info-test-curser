using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.LicensePlateTypes;
using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.LicensePlateType.GetAll;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Fetch;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Commands.Fetch;

public sealed class FetchMultiTenantLicensePlateTypesFromSourceHandler
    : CommandHandler<FetchMultiTenantLicensePlateTypesFromSourceCommand>
{
    private readonly ICoreInsuranceGetAllLicensePlateTypesCaller _caller;
    private readonly ILicensePlateTypeCommandRepository _commandRepository;

    public FetchMultiTenantLicensePlateTypesFromSourceHandler(ZaminServices zaminServices,
        ICoreInsuranceGetAllLicensePlateTypesCaller caller,
        ILicensePlateTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _caller = caller;
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantLicensePlateTypesFromSourceCommand command)
    {
        var sourceDataResponse = await _caller.Call(new GetAllLicensePlateTypesRequest());

        if (sourceDataResponse.IsFailure)
            throw new InvalidOperationException(sourceDataResponse.Error);

        var items = sourceDataResponse.Value!;
        var nextPriority = await _commandRepository.GetNextPriority();

        foreach (var item in items)
        {
            var rawTitle = string.IsNullOrWhiteSpace(item.sharh) ? item.NoePelakID.ToString() : item.sharh;
            var coreId = CoreId.FromString(item.NoePelakID.ToString());
            var existing = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(coreId);

            if (existing is null)
            {
                var created = LicensePlateType.Create(new CreateLicensePlateTypeParameter(
                    DIPTitle.FromString(rawTitle),
                    NullableTitle.FromString((string?)null),
                    coreId,
                    Priority.FromLong(nextPriority++)));
                await _commandRepository.InsertAsync(created);
                continue;
            }

            var title = DIPTitle.FromString(rawTitle);
            var updateParameter = new UpdateLicensePlateTypeParameter(title, DIPTitle.FromString(rawTitle),
                existing.Priority);
            existing.Update(updateParameter);
        }

        await _commandRepository.CommitAsync();
        return Ok();
    }
}
