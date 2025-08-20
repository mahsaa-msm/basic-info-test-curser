using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Countries.Queries.GetById;

public sealed class GetCountryByIdQuery : IQuery<CountryQr>, IWebRequest
{
    public long CountryId { get; set; }

    public string Path => "/Api/Country/GetCountryById";
}