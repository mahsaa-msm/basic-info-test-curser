using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.TravelPassengerCountTypes;

public class TravelPassengerCountTypeCommandRepository : BaseCommandRepository<TravelPassengerCountType, MasterDataCommandDbContext, long>,
    ITravelPassengerCountTypeCommandRepository
{
    public TravelPassengerCountTypeCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<TravelPassengerCountType>> GetAllAsync()
    {
        return await _dbContext.TravelPassengerCountTypes.ToListAsync();
    }

    public async Task<int> GetMaxPriorityAsync()
    {
        return await _dbContext.TravelPassengerCountTypes.MaxAsync(c => (int?)c.Priority) ?? 1;
    }

    public async Task InsertRangeAsync(List<TravelPassengerCountType> countries)
    {
        await _dbContext.TravelPassengerCountTypes.AddRangeAsync(countries);
    }
}
