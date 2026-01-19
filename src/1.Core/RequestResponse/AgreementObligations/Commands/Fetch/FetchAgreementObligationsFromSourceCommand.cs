using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.AgreementObligations.Commands.Fetch;

public sealed class FetchAgreementObligationsFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/AgreementObligation/FetchAgreementObligationsFromSource";
}
