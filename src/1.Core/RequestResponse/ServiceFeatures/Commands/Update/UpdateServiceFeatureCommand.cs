using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Update;

public sealed class UpdateServiceFeatureCommand : ICommand, IWebRequest
{
    public long ServiceFeatureId { get; set; }
    public string? Description { get; set; }

    public string Path => "/Api/ServiceFeature/UpdateServiceFeature";
}