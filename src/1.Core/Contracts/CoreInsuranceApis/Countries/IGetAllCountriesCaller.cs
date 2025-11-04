using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.Countries;
public interface IGetAllCountriesCaller
{
    Task<Response<List<GetAllCountriesResponse>>> Call(GetAllCountriesRequest request);
}
