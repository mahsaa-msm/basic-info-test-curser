using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleColor.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleColors;

public interface ICoreInsuranceGetAllVehicleColorsCaller
{
    Task<Response<List<GetAllVehicleColorsResponse>>> Call(GetAllVehicleColorsRequest request);
}

