using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAllByKey;

public sealed class GetAllServiceFeaturesByKeyQuery : IQuery<GetAllServiceFeaturesByKeyQr?>, IWebRequest
{
    public ServiceFeatureCategory Key { get; set; }

    public string Path => "/Api/ServiceFeature/GetAllServiceFeaturesByKey";
}

