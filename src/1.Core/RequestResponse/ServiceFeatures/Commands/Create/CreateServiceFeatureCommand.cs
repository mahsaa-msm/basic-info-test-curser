using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Create;

public sealed class CreateServiceFeatureCommand : ICommand<long>, IWebRequest
{
    public ServiceFeatureKey Key { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string Path => "/Api/ServiceFeature/CreateServiceFeature";
}