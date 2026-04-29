using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
public sealed class FetchIssuanceSchemesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/IssuanceScheme/FetchIssuanceSchemesFromSource";
}

