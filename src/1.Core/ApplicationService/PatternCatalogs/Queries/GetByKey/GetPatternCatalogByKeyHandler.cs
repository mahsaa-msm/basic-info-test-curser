using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Queries;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetByKey;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.PatternCatalogs.Queries.GetByKey;

public sealed class GetPatternCatalogByKeyHandler : QueryHandler<GetPatternCatalogByKeyQuery, PatternCatalogQr?>
{
    private readonly IPatternCatalogQueryRepository _patternCatalogQueryRepository;

    public GetPatternCatalogByKeyHandler(ZaminServices zaminServices,
                                         IPatternCatalogQueryRepository patternCatalogQueryRepository)
        : base(zaminServices)
    {
        _patternCatalogQueryRepository = patternCatalogQueryRepository;
    }

    public override async Task<QueryResult<PatternCatalogQr?>> Handle(GetPatternCatalogByKeyQuery query)
        => Result(await _patternCatalogQueryRepository.Execute(query));
}

