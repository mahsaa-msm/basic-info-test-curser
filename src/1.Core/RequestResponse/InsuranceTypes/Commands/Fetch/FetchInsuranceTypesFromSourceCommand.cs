using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Fetch;

public sealed class FetchInsuranceTypesFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/InsuranceType/FetchInsuranceTypesFromSource";
}
