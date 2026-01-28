using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
public sealed class FetchIssuanceSchemesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/IssuanceScheme/FetchIssuanceSchemesFromSource";
}
