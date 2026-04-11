using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Create;

public sealed class CreateServiceFeatureCommand : ICommand<long>, IWebRequest
{
    public ServiceFeatureCategory Key { get; set; }
    public string? Description { get; set; }

    public string Path => "/Api/ServiceFeature/CreateServiceFeature";
}