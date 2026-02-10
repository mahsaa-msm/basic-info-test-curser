using Master.Data.Core.Contracts.InsuranceUnits.Queries;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceUnits.Queries.GetAllInArea;

public sealed class GetAllInsuranceUnitsInAreaHandler : QueryHandler<GetAllInsuranceUnitsInAreaQuery, List<InsuranceUnitMapItemQr>>
{
    private readonly IInsuranceUnitQueryRepository _insuranceUnitQueryRepository;

    public GetAllInsuranceUnitsInAreaHandler(ZaminServices zaminServices,
                                       IInsuranceUnitQueryRepository insuranceUnitQueryRepository)
        : base(zaminServices)
    {
        _insuranceUnitQueryRepository = insuranceUnitQueryRepository;
    }

    public override async Task<QueryResult<List<InsuranceUnitMapItemQr>>> Handle(GetAllInsuranceUnitsInAreaQuery query)
        => Result(await _insuranceUnitQueryRepository.Execute(query));
}
