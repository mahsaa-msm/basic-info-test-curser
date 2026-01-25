using Master.Data.Core.Contracts.InsuranceUnits.Queries;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceUnits.Queries.GetAllPagedFilter;

public sealed class GetAllInsuranceUnitsPagedFilterHandler : QueryHandler<GetAllInsuranceUnitsPagedFilterQuery, PagedData<InsuranceUnitListItemQr>>
{
    private readonly IInsuranceUnitQueryRepository _insuranceUnitQueryRepository;

    public GetAllInsuranceUnitsPagedFilterHandler(ZaminServices zaminServices,
                                                  IInsuranceUnitQueryRepository insuranceUnitQueryRepository)
        : base(zaminServices)
    {
        _insuranceUnitQueryRepository = insuranceUnitQueryRepository;
    }

    public override async Task<QueryResult<PagedData<InsuranceUnitListItemQr>>> Handle(GetAllInsuranceUnitsPagedFilterQuery query)
        => Result(await _insuranceUnitQueryRepository.Execute(query));
}