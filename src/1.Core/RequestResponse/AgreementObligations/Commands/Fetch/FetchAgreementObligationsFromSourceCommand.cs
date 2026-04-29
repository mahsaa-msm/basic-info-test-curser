using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.Fetch;

public sealed class FetchAgreementObligationsFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/AgreementObligation/FetchAgreementObligationsFromSource";
}

