using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetById;

public sealed class GetCityByIdQuery : IQuery<CityQr>, IWebRequest
{
    public long CityId { get; set; }

    public string Path => "/Api/City/GetCityById";
}
