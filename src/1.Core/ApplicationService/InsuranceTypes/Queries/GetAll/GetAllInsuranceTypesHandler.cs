using Master.Data.Core.Contracts.InsuranceTypes.Queries;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Queries.GetAll;

public class GetAllInsuranceTypesHandler : QueryHandler<GetAllInsuranceTypeQuery, List<InsuranceTypeSelectItemQr>>
{
    private readonly IInsuranceTypeQueryRepository _insuranceTypeQueryRepository;

    public GetAllInsuranceTypesHandler(ZaminServices zaminServices,
                                  IInsuranceTypeQueryRepository insuranceTypeQueryRepository)
        : base(zaminServices)
    {
        _insuranceTypeQueryRepository = insuranceTypeQueryRepository;
    }

    public override async Task<QueryResult<List<InsuranceTypeSelectItemQr>>> Handle(GetAllInsuranceTypeQuery query)
        => Result(await _insuranceTypeQueryRepository.Execute(query));
}
