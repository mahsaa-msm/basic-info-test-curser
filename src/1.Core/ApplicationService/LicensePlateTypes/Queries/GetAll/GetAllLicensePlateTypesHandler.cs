using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Queries.GetAll;

public sealed class GetAllLicensePlateTypesHandler : QueryHandler<GetAllLicensePlateTypeQuery, List<LicensePlateTypeSelectItemQr>>
{
    private readonly ILicensePlateTypeQueryRepository _queryRepository;

    public GetAllLicensePlateTypesHandler(ZaminServices zaminServices,
        ILicensePlateTypeQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<List<LicensePlateTypeSelectItemQr>>> Handle(GetAllLicensePlateTypeQuery query)
        => Result(await _queryRepository.Execute(query));
}
