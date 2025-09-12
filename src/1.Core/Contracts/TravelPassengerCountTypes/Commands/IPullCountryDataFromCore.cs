using Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelPassengerCountTypes;

namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;

public interface IPullTravelPassengerCountTypeDataFromCore
{
    public Task<List<TravelPassengerCountTypeModel>> ExecuteAsync();
}
