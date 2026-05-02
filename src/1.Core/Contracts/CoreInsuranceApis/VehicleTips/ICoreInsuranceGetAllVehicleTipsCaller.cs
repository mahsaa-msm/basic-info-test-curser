using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleTip.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleTips;

public interface ICoreInsuranceGetAllVehicleTipsCaller
{
    Task<Response<List<GetAllVehicleTipsResponseItem>>> Call(GetAllVehicleTipsRequest request);
}
