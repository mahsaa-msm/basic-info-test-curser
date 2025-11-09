using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Cities.Commands.Fetch;
public sealed class FetchCitiesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/City/FetchCitiesFromSource";
}
