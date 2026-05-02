using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Queries.GetAllPagedFilter;

public sealed class GetAllLicensePlateTypesPagedFilterHandler
    : QueryHandler<GetAllLicensePlateTypesPagedFilterQuery, PagedData<LicensePlateTypeListItemQr>>
{
    private readonly ILicensePlateTypeQueryRepository _queryRepository;

    public GetAllLicensePlateTypesPagedFilterHandler(ZaminServices zaminServices,
        ILicensePlateTypeQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<LicensePlateTypeListItemQr>>> Handle(
        GetAllLicensePlateTypesPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}
