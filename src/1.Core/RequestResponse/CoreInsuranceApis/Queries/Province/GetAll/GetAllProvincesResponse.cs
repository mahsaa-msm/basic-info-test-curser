using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Common;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;

public sealed class GetAllProvincesResponse
{
    public string naamOstan { get; set; } = string.Empty;
    public string codeOstan { get; set; } = string.Empty;
    public long ostanID { get; set; }
    public long keshvarID { get; set; }
    public string keshvarNaam { get; set; } = string.Empty;

    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    public bool hasError { get; set; }
    public List<CoreInsuranceValidation> validations { get; set; } = new();
}

