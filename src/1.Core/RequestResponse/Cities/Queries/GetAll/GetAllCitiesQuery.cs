using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
public sealed class GetAllCitiesQuery : IQuery<List<CitySelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/City/GetAllCities";
}
