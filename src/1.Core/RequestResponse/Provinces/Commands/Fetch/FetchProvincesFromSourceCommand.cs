using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Fetch;

public sealed class FetchProvincesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/Province/FetchProvincesFromSource";
}

