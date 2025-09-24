using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetById;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetAll;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetPagedFilter;
using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;

namespace Master.Data.Infra.Data.Sql.Queries.TravelDurationTypes;

public class TravelDurationTypeQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    ITravelDurationTypesQueryRepository
{
    public TravelDurationTypeQueryRepository(MasterDataQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TravelDurationTypesQr?> ExecuteAsync(GetTravelDurationTypesByIdQuery query)
    {
        return await _dbContext.TravelDurationTypes.Select(TravelDurationType => new TravelDurationTypesQr
        {
            CoreId = TravelDurationType.CoreId,
            Title = TravelDurationType.Title.Value,
            Id = TravelDurationType.Id,
            IsEnable = TravelDurationType.IsEnable,
            Priority = TravelDurationType.Priority.Value,
        }).FirstOrDefaultAsync(c => c.Id == query.Id);
    }

    public async Task<List<TravelDurationTypesItemQr>> ExecuteAsync(GetAllTravelDurationTypesQuery query)
    {
        return await _dbContext.TravelDurationTypes.Where(c => c.IsEnable).OrderBy(c => c.Priority).Select(c => new TravelDurationTypesItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            Title = c.Title.Value,
        }).ToListAsync();
    }

    public async Task<PagedData<TravelDurationTypesQr>> ExecuteAsync(GetTravelDurationTypesPagedFilterQuery query)
    {
        var filter = _dbContext.TravelDurationTypes.AsQueryable();

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(i => i.Title.Value.Contains(query.Title));

        if (query.CoreId != default)
            filter = filter.Where(i => i.CoreId == query.CoreId);

        if (query.IsEnable.HasValue)
            filter = filter.Where(i => i.IsEnable == query.IsEnable);

        if (query.Priority.HasValue)
            filter = filter.Where(i => i.Priority.Value == query.Priority);

        var result = await filter.ToPagedData(query, item => new TravelDurationTypesQr
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
