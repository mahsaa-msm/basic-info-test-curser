using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;

public sealed class GetAllInsuranceUnitsPagedFilterQuery : PageQuery<PagedData<InsuranceUnitListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }
    public long? Latitude { get; set; }
    public long? Longitude { get; set; }
    public string? ProvinceCoreId { get; set; }
    public InsuranceUnitType? Type { get; set; }
    public InsuranceUnitState? State { get; set; }
    public string? CityCoreId { get; set; }

    public string Path => "/Api/InsuranceUnit/GetAllInsuranceUnitsPagedFilter";
}