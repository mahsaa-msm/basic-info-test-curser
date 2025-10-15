using Master.Data.Core.Contracts.ExternalAPI.Common.Responses;

namespace Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelDurationTypes;

public class TravelDurationTypesResponse : BaseCoreInsuranceResponse
{
    public List<TravelDurationTypesModel> itemList { get; set; } = [];
}

public class TravelDurationTypesModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
}

