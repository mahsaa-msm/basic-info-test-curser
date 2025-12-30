using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetPagedFilter;

public class GetTravelDurationTypesPagedFilterQuery : PageQuery<PagedData<TravelDurationTypesQr>>
{
    public int CoreId { get; set; }
    public string? Title { get; set; }
    public int? Priority { get; set; }
    public bool? IsEnable { get; set; }
}
