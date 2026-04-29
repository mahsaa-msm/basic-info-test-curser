using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Fetch;

public sealed class FetchInsuranceTypesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/InsuranceType/FetchInsuranceTypesFromSource";
}

