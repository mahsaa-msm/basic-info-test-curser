using Master.Data.Core.Contracts.AgreementObligations.Queries;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Queries.GetById;

public sealed class GetAgreementObligationByIdHandler : QueryHandler<GetAgreementObligationByIdQuery, AgreementObligationQr?>
{
    private readonly IAgreementObligationQueryRepository _agreementObligationQueryRepository;

    public GetAgreementObligationByIdHandler(ZaminServices zaminServices,
                                             IAgreementObligationQueryRepository agreementObligationQueryRepository)
        : base(zaminServices)
    {
        _agreementObligationQueryRepository = agreementObligationQueryRepository;
    }

    public override async Task<QueryResult<AgreementObligationQr?>> Handle(GetAgreementObligationByIdQuery query)
        => Result(await _agreementObligationQueryRepository.ExecuteAsync(query));
}
