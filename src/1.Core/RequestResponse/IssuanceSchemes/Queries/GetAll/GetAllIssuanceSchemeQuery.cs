using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAll;

public sealed class GetAllIssuanceSchemeQuery : IQuery<List<IssuanceSchemeSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/IssuanceScheme/GetAllIssuanceSchemes";
}