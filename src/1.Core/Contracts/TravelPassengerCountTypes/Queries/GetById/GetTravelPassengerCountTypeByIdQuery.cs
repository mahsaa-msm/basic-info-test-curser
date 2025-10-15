using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetById;
public sealed class GetTravelPassengerCountTypeByIdQuery : IQuery<TravelPassengerCountTypeQr>
{
    public int Id { get; set; }
}
