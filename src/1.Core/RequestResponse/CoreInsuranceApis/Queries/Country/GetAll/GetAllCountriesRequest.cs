using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;

public sealed class GetAllCountriesRequest : IWebRequest
{
    public string Path => "/v2_0/keshvar/all";
}
