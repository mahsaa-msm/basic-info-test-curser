using Vehicle.Insurance.Core.Contracts.InsuranceTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.InsuranceTypes.Queries.GetById;

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

