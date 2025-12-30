using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.TravelDurationTypes;

public class TravelDurationTypesCommandRepository : BaseCommandRepository<TravelDurationType, MasterDataCommandDbContext, long>,
    ITravelDurationTypesCommandRepository
{
    public TravelDurationTypesCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<TravelDurationType>> GetAllAsync()
    {
        return await _dbContext.TravelDurationTypes.ToListAsync();
    }

    public async Task<int> GetMaxPriorityAsync()
    {
        return await _dbContext.TravelDurationTypes.MaxAsync(c => (int?)c.Priority) ?? 1;
    }

    public async Task InsertRangeAsync(List<TravelDurationType> types)
    {
        await _dbContext.TravelDurationTypes.AddRangeAsync(types);
    }
}
