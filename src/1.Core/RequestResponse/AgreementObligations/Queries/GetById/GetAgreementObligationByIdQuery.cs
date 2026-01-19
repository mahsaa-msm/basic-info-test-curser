using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;

public sealed class GetAgreementObligationByIdQuery : IQuery<AgreementObligationQr?>, IWebRequest
{
    public long AgreementObligationId { get; set; }

    public string Path => "/Api/AgreementObligation/GetAgreementObligationById";
}