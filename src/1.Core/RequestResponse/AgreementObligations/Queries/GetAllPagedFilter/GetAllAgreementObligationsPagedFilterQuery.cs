using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;

public sealed class GetAllAgreementObligationsPagedFilterQuery : PageQuery<PagedData<AgreementObligationListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }
    public string? AgreementObligationNumber { get; set; }
    public string? AgreementNumber { get; set; }
    public string? AgreementCoreId { get; set; }
    public string? InsuranceTypeCoreId { get; set; }

    public string Path => "/Api/AgreementObligation/GetAllAgreementObligationsPagedFilter";
}
