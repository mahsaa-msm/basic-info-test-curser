using Master.Data.Core.Contracts.IssuanceSchemes.Queries;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.IssuanceSchemes.Queries.GetAllPagedFilter;

public class GetAllIssuanceSchemesPagedFilterHandler : QueryHandler<GetAllIssuanceSchemesPagedFilterQuery, PagedData<IssuanceSchemeListItemQr>>
{
    private readonly IIssuanceSchemeQueryRepository _queryRepository;

    public GetAllIssuanceSchemesPagedFilterHandler(ZaminServices zaminServices,
                                             IIssuanceSchemeQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<IssuanceSchemeListItemQr>>> Handle(GetAllIssuanceSchemesPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}