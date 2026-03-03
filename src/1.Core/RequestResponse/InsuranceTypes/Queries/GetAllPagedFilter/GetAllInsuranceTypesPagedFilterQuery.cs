using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;

public sealed class GetAllInsuranceTypesPagedFilterQuery : PageQuery<PagedData<InsuranceTypeListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/InsuranceType/GetAllInsuranceTypesPagedFilter";
}