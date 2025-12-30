using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Countries.Queries.GetAll;

public sealed class GetAllCountryQuery : IQuery<List<CountrySelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/Country/GetAllCountries";
}