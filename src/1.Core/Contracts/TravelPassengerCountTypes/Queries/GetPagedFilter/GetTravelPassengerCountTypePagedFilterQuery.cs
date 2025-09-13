using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;

public class GetTravelPassengerCountTypePagedFilterQuery : PageQuery<PagedData<TravelPassengerCountTypeQr>>
{
    public int CoreId { get; set; }
    public string? Title { get; set; }
    public int? Priority { get; set; }
    public bool? IsEnable { get; set; }
}
