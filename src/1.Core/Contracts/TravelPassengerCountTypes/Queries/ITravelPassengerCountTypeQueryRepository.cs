using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetAll;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetById;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;

public interface ITravelPassengerCountTypeQueryRepository : IQueryRepository
{
    public Task<TravelPassengerCountTypeQr?> ExecuteAsync(GetTravelPassengerCountTypeByIdQuery query);
    public Task<List<TravelPassengerCountTypeItemQr>> ExecuteAsync(GetAllTravelPassengerCountTypeQuery query);
    public Task<PagedData<TravelPassengerCountTypeQr>> ExecuteAsync(GetTravelPassengerCountTypePagedFilterQuery query);
}
