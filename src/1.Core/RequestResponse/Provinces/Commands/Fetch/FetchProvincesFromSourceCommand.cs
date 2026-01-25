using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Fetch;

public sealed class FetchProvincesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/Province/FetchProvincesFromSource";
}
