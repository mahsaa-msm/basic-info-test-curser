using Master.Data.Core.Contracts.PatternCatalogs.Queries;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.PatternCatalogs.Queries.GetAll;

public sealed class GetAllPatternCatalogsHandler : QueryHandler<GetAllPatternCatalogsQuery, List<PatternCatalogQr>>
{
    private readonly IPatternCatalogQueryRepository _patternCatalogQueryRepository;

    public GetAllPatternCatalogsHandler(ZaminServices zaminServices,
                                        IPatternCatalogQueryRepository patternCatalogQueryRepository)
        : base(zaminServices)
    {
        _patternCatalogQueryRepository = patternCatalogQueryRepository;
    }

    public override async Task<QueryResult<List<PatternCatalogQr>>> Handle(GetAllPatternCatalogsQuery query)
        => Result(await _patternCatalogQueryRepository.Execute(query));
}
