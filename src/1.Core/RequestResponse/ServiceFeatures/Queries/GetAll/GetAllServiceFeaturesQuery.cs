using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAll;

public sealed class GetAllServiceFeaturesQuery : IQuery<List<ServiceFeatureQr>>, IWebRequest
{
    public bool? IsActive { get; set; }
    public int? Level { get; set; } = 3;

    public string Path => "/Api/ServiceFeature/GetAllServiceFeatures";
}
