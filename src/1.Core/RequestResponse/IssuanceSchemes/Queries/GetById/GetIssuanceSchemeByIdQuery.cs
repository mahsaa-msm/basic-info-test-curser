using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetById;

public sealed class GetIssuanceSchemeByIdQuery : IQuery<IssuanceSchemeQr>, IWebRequest
{
    public long IssuanceSchemeId { get; set; }

    public string Path => "/Api/IssuanceScheme/GetIssuanceSchemeById";
}
