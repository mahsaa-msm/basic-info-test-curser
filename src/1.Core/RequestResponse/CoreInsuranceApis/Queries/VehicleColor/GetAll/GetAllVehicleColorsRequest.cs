using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleColor.GetAll;

public sealed class GetAllVehicleColorsRequest : IWebRequest
{
    public string Path => "/rangBadanah/findByFilter";
}

