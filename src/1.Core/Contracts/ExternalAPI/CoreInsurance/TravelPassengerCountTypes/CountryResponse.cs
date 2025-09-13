using Master.Data.Core.Contracts.ExternalAPI.Common.Responses;

namespace Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelPassengerCountTypes;

public class TravelPassengerCountTypeResponse : BaseCoreInsuranceResponse
{
    public List<TravelPassengerCountTypeModel> itemList { get; set; } =[];
}
public class TravelPassengerCountTypeModel
{
    public int Id { get; set; }
    public string? Title { get; set; }

}