using Master.Data.Core.Contracts.AgreementObligations.Queries;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Queries.GetAll;

public sealed class GetAllAgreementObligationsHandler : QueryHandler<GetAllAgreementObligationsQuery, List<AgreementObligationSelectItemQr>>
{
    private readonly IAgreementObligationQueryRepository _agreementObligationQueryRepository;

    public GetAllAgreementObligationsHandler(ZaminServices zaminServices,
                                             IAgreementObligationQueryRepository agreementObligationQueryRepository)
        : base(zaminServices)
    {
        _agreementObligationQueryRepository = agreementObligationQueryRepository;
    }

    public override async Task<QueryResult<List<AgreementObligationSelectItemQr>>> Handle(GetAllAgreementObligationsQuery query)
        => Result(await _agreementObligationQueryRepository.ExecuteAsync(query));
}
