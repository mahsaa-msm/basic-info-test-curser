using Master.Data.Core.RequestResponse.Cities.Queries.CommanResults;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
public sealed class GetAllCityQuery : IQuery<List<CityItemQr>>
{
    public int? StateId { get; set; }
}
