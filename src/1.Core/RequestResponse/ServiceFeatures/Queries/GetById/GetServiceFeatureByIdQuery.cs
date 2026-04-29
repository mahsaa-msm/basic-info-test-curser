using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetById;

public sealed class GetServiceFeatureByIdQuery : IQuery<ServiceFeatureQr?>, IWebRequest
{
    public long ServiceFeatureId { get; set; }

    public string Path => "/Api/ServiceFeature/GetServiceFeatureById";
}
