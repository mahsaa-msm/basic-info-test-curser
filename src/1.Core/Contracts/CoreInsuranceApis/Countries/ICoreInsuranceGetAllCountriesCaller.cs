using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Common;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Countries;

public interface ICoreInsuranceGetAllCountriesCaller
{
    Task<Response<BaseCoreInsuranceResponse<GetAllCountriesResponse>>> Call(GetAllCountriesRequest request);
}

