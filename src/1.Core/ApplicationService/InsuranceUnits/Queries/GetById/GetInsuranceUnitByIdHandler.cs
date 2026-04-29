using Vehicle.Insurance.Core.Contracts.InsuranceUnits.Queries;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.InsuranceUnits.Queries.GetById;

public sealed class GetInsuranceUnitByIdHandler : QueryHandler<GetInsuranceUnitByIdQuery, InsuranceUnitQr>
{
    private readonly IInsuranceUnitQueryRepository _insuranceUnitQueryRepository;

    public GetInsuranceUnitByIdHandler(ZaminServices zaminServices,
                                       IInsuranceUnitQueryRepository insuranceUnitQueryRepository)
        : base(zaminServices)
    {
        _insuranceUnitQueryRepository = insuranceUnitQueryRepository;
    }

    public override async Task<QueryResult<InsuranceUnitQr>> Handle(GetInsuranceUnitByIdQuery query)
        => Result(await _insuranceUnitQueryRepository.Execute(query));
}

