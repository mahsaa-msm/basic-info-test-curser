using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Common;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.Countries;

public interface ICoreInsuranceGetAllCountriesCaller
{
    Task<Response<BaseCoreInsuranceResponse<GetAllCountriesResponse>>> Call(GetAllCountriesRequest request);
}
