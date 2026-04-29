using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Fetch;

public sealed class FetchCountriesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/Country/FetchCountriesFromSource";
}

