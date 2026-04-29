using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Fetch;

public sealed class FetchCitiesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/City/FetchCitiesFromSource";
}

