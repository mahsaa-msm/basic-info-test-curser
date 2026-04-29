using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Cities;

public interface ICoreInsuranceGetAllCitiesCaller
{
    Task<Response<List<GetAllCitiesResponse>>> Call(GetAllCitiesRequest request);
}

