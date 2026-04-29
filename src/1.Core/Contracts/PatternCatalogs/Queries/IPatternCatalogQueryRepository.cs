using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetByKey;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.PatternCatalogs.Queries;

public interface IPatternCatalogQueryRepository : IQueryRepository
{
    Task<PatternCatalogQr?> Execute(GetPatternCatalogByIdQuery query);
    Task<PatternCatalogQr?> Execute(GetPatternCatalogByKeyQuery query);
    Task<PagedData<PatternCatalogQr>> Execute(GetAllPatternCatalogsPagedFilterQuery query);
    Task<List<PatternCatalogQr>> Execute(GetAllPatternCatalogsQuery query);
}

