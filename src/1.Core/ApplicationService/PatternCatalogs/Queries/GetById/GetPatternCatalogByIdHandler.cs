using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Queries;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.PatternCatalogs.Queries.GetById;

public sealed class GetPatternCatalogByIdHandler : QueryHandler<GetPatternCatalogByIdQuery, PatternCatalogQr?>
{
    private readonly IPatternCatalogQueryRepository _patternCatalogQueryRepository;

    public GetPatternCatalogByIdHandler(ZaminServices zaminServices,
                                        IPatternCatalogQueryRepository patternCatalogQueryRepository)
        : base(zaminServices)
    {
        _patternCatalogQueryRepository = patternCatalogQueryRepository;
    }

    public override async Task<QueryResult<PatternCatalogQr?>> Handle(GetPatternCatalogByIdQuery query)
        => Result(await _patternCatalogQueryRepository.Execute(query));
}

