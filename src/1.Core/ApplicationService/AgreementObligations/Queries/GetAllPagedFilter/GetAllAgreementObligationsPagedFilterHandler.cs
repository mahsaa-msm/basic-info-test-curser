using Vehicle.Insurance.Core.Contracts.AgreementObligations.Queries;
using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.AgreementObligations.Queries.GetAllPagedFilter;

public sealed class GetAllAgreementObligationsPagedFilterHandler : QueryHandler<GetAllAgreementObligationsPagedFilterQuery, PagedData<AgreementObligationListItemQr>>
{
    private readonly IAgreementObligationQueryRepository _agreementObligationQueryRepository;

    public GetAllAgreementObligationsPagedFilterHandler(ZaminServices zaminServices,
                                                        IAgreementObligationQueryRepository agreementObligationQueryRepository)
        : base(zaminServices)
    {
        _agreementObligationQueryRepository = agreementObligationQueryRepository;
    }

    public override async Task<QueryResult<PagedData<AgreementObligationListItemQr>>> Handle(GetAllAgreementObligationsPagedFilterQuery query)
        => Result(await _agreementObligationQueryRepository.ExecuteAsync(query));
}

