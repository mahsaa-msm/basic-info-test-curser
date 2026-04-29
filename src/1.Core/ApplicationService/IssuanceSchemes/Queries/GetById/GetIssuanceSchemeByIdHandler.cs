using Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Queries;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.IssuanceSchemes.Queries.GetById;

public class GetIssuanceSchemeByIdHandler : QueryHandler<GetIssuanceSchemeByIdQuery, IssuanceSchemeQr>
{
    private readonly IIssuanceSchemeQueryRepository _queryRepository;

    public GetIssuanceSchemeByIdHandler(ZaminServices zaminServices,
                                 IIssuanceSchemeQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<IssuanceSchemeQr>> Handle(GetIssuanceSchemeByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}

