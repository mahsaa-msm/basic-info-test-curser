using Master.Data.Core.Contracts.IssuanceSchemes.Queries;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.IssuanceSchemes.Queries.GetAll;

public class GetAllIssuanceSchemesHandler : QueryHandler<GetAllIssuanceSchemeQuery, List<IssuanceSchemeSelectItemQr>>
{
    private readonly IIssuanceSchemeQueryRepository _issuanceSchemeQueryRepository;

    public GetAllIssuanceSchemesHandler(ZaminServices zaminServices,
                                  IIssuanceSchemeQueryRepository issuanceSchemeQueryRepository)
        : base(zaminServices)
    {
        _issuanceSchemeQueryRepository = issuanceSchemeQueryRepository;
    }

    public override async Task<QueryResult<List<IssuanceSchemeSelectItemQr>>> Handle(GetAllIssuanceSchemeQuery query)
        => Result(await _issuanceSchemeQueryRepository.Execute(query));
}
