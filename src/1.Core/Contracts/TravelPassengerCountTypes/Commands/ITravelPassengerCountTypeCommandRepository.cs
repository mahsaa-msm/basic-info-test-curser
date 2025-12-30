using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;

public interface ITravelPassengerCountTypeCommandRepository : ICommandRepository<TravelPassengerCountType, long>
{
    public Task<List<TravelPassengerCountType>> GetAllAsync();
    public Task InsertRangeAsync(List<TravelPassengerCountType> travelPassengerCountTypes);
    public Task<int> GetMaxPriorityAsync();
}
