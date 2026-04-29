using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Common;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;

public sealed class GetAllCitiesResponse
{
    public string naamShahr { get; set; } = string.Empty;
    //public string codeShahr { get; set; } = string.Empty;
    public long shahrID { get; set; }
    public long ostanID { get; set; }
    public string naamOstan { get; set; } = string.Empty;

    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    public bool hasError { get; set; }
    public List<CoreInsuranceValidation> validations { get; set; } = new();
}

