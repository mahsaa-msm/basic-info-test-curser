using Master.Data.Core.Contracts.InsuranceTypes.Queries;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Queries.GetAllPagedFilter;

public class GetAllInsuranceTypesPagedFilterHandler : QueryHandler<GetAllInsuranceTypesPagedFilterQuery, PagedData<InsuranceTypeListItemQr>>
{
    private readonly IInsuranceTypeQueryRepository _queryRepository;

    public GetAllInsuranceTypesPagedFilterHandler(ZaminServices zaminServices,
                                             IInsuranceTypeQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<InsuranceTypeListItemQr>>> Handle(GetAllInsuranceTypesPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}