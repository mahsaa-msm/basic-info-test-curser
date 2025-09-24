using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetById;
public sealed class GetTravelDurationTypesByIdQuery : IQuery<TravelDurationTypesQr>
{
    public int Id { get; set; }
}
