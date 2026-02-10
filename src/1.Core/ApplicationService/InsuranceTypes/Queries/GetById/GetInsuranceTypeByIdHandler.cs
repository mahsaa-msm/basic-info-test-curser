using Master.Data.Core.Contracts.InsuranceTypes.Queries;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Queries.GetById;

public class GetInsuranceTypeByIdHandler : QueryHandler<GetInsuranceTypeByIdQuery, InsuranceTypeQr>
{
    private readonly IInsuranceTypeQueryRepository _queryRepository;

    public GetInsuranceTypeByIdHandler(ZaminServices zaminServices,
                                 IInsuranceTypeQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<InsuranceTypeQr>> Handle(GetInsuranceTypeByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}
