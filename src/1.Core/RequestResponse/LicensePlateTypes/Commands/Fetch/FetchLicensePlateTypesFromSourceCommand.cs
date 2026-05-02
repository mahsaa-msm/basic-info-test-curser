using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Fetch;

public sealed class FetchLicensePlateTypesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/LicensePlateType/FetchLicensePlateTypesFromSource";
}
