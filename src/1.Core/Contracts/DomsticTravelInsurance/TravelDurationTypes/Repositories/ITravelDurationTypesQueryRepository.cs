using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetAll;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetById;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;

public interface ITravelDurationTypesQueryRepository : IQueryRepository
{
    public Task<TravelDurationTypesQr?> ExecuteAsync(GetTravelDurationTypesByIdQuery query);
    public Task<List<TravelDurationTypesItemQr>> ExecuteAsync(GetAllTravelDurationTypesQuery query);
    public Task<PagedData<TravelDurationTypesQr>> ExecuteAsync(GetTravelDurationTypesPagedFilterQuery query);
}
