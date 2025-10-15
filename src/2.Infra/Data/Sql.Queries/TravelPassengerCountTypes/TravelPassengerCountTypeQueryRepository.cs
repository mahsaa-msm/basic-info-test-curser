using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetAll;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetById;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes;

public class TravelPassengerCountTypeQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    ITravelPassengerCountTypeQueryRepository
{
    public TravelPassengerCountTypeQueryRepository(MasterDataQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TravelPassengerCountTypeQr?> ExecuteAsync(GetTravelPassengerCountTypeByIdQuery query)
    {
        return await _dbContext.TravelPassengerCountTypes.Select(travelPassengerCountType => new TravelPassengerCountTypeQr
        {
            CoreId = travelPassengerCountType.CoreId,
            Title = travelPassengerCountType.Title.Value,
            Id = travelPassengerCountType.Id,
            IsEnable = travelPassengerCountType.IsEnable,
            Priority = travelPassengerCountType.Priority.Value,
        }).FirstOrDefaultAsync(c => c.Id == query.Id);
    }

    public async Task<List<TravelPassengerCountTypeItemQr>> ExecuteAsync(GetAllTravelPassengerCountTypeQuery query)
    {
        return await _dbContext.TravelPassengerCountTypes.Where(c => c.IsEnable).OrderBy(c => c.Priority).Select(c => new TravelPassengerCountTypeItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            Title = c.Title.Value,
        }).ToListAsync();
    }

    public async Task<PagedData<TravelPassengerCountTypeQr>> ExecuteAsync(GetTravelPassengerCountTypePagedFilterQuery query)
    {
        var filter = _dbContext.TravelPassengerCountTypes.AsQueryable();

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(i => i.Title.Value.Contains(query.Title));

        if (query.CoreId != default)
            filter = filter.Where(i => i.CoreId == query.CoreId);

        if (query.IsEnable.HasValue)
            filter = filter.Where(i => i.IsEnable == query.IsEnable);

        if (query.Priority.HasValue)
            filter = filter.Where(i => i.Priority.Value == query.Priority);

        var result = await filter.ToPagedData(query, item => new TravelPassengerCountTypeQr
        {
            Title = item.Title.Value,
            CoreId = item.CoreId,
            Id = item.Id,
            IsEnable = item.IsEnable,
            Priority = item.Priority.Value,
        });
        return result;
    }
}
