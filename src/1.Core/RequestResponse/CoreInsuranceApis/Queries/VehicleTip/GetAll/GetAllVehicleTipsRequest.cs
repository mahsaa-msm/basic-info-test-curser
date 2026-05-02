using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleTip.GetAll;

public sealed class GetAllVehicleTipsRequest : IWebRequest
{
    public string Path => "/tip/findByFilter";
}
