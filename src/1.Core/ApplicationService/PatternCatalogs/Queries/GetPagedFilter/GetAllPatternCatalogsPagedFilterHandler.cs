using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Queries;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.PatternCatalogs.Queries.GetPagedFilter;

public sealed class GetAllPatternCatalogsPagedFilterHandler : QueryHandler<GetAllPatternCatalogsPagedFilterQuery, PagedData<PatternCatalogQr>>
{
    private readonly IPatternCatalogQueryRepository _patternCatalogQueryRepository;

    public GetAllPatternCatalogsPagedFilterHandler(ZaminServices zaminServices,
                                                   IPatternCatalogQueryRepository patternCatalogQueryRepository)
        : base(zaminServices)
    {
        _patternCatalogQueryRepository = patternCatalogQueryRepository;
    }

    public override async Task<QueryResult<PagedData<PatternCatalogQr>>> Handle(GetAllPatternCatalogsPagedFilterQuery query)
        => Result(await _patternCatalogQueryRepository.Execute(query));
}

