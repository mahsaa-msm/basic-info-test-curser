using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Queries;
public interface IIssuanceSchemeQueryRepository : IQueryRepository
{
    Task<IssuanceSchemeQr> Execute(GetIssuanceSchemeByIdQuery query);
    Task<List<IssuanceSchemeSelectItemQr>> Execute(GetAllIssuanceSchemeQuery query);
    Task<PagedData<IssuanceSchemeListItemQr>> Execute(GetAllIssuanceSchemesPagedFilterQuery query);
}

