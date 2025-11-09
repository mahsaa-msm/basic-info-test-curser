using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
public sealed class FetchCountriesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/Country/FetchCountriesFromSource";
}
