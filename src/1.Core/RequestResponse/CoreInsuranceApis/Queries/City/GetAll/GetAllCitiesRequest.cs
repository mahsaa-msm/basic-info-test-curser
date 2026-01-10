using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;
public sealed class GetAllCitiesRequest : IWebRequest
{
    public string Path => "/shahrvi/findByFilter";
}
