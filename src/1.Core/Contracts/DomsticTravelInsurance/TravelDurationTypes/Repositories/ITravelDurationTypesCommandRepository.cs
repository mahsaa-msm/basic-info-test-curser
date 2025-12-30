using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;

public interface ITravelDurationTypesCommandRepository : ICommandRepository<TravelDurationType, long>
{
    public Task<List<TravelDurationType>> GetAllAsync();
    public Task InsertRangeAsync(List<TravelDurationType> TravelDurationTypes);
    public Task<int> GetMaxPriorityAsync();
}
