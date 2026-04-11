using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;

public sealed class GetAllServiceFeaturesPagedFilterQuery : PageQuery<PagedData<ServiceFeatureQr>>, IWebRequest
{
    public ServiceFeatureCategory? Key { get; set; }
    public string? ServiceName { get; set; }
    public string? FeatureName { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string Path => "/Api/ServiceFeature/GetAllServiceFeaturesPagedFilter";
}