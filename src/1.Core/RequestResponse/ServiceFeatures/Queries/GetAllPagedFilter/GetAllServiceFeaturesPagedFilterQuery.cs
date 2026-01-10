using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;

public sealed class GetAllServiceFeaturesPagedFilterQuery : PageQuery<PagedData<ServiceFeatureQr>>, IWebRequest
{
    public ServiceFeatureKey? Key { get; set; }
    public string? ServiceName { get; set; }
    public string? FeatureName { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string Path => "/Api/ServiceFeature/GetAllServiceFeaturesPagedFilter";
}