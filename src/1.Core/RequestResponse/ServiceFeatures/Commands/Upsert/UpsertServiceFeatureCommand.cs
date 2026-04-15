using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Upsert;

public sealed class UpsertServiceFeatureCommand : ICommand, IWebRequest
{
    public ServiceFeatureCategory Key { get; set; }
    public List<long> TenantIds { get; set; } = [];


    public string Path => "/Api/ServiceFeature/UpsertServiceFeature";
}
