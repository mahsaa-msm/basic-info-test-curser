using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;

public sealed class GetAllAgreementObligationsQuery : IQuery<List<AgreementObligationSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/AgreementObligation/GetAllAgreementObligations";
}
