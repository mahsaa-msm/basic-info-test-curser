using Master.Data.Core.Contracts.PatternCatalogs.Queries;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.PatternCatalogs.Queries.GetById;

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
