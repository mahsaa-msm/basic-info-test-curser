using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.Cities;

public interface ICoreInsuranceGetAllCitiesCaller
{
    Task<Response<List<GetAllCitiesResponse>>> Call(GetAllCitiesRequest request);
}
