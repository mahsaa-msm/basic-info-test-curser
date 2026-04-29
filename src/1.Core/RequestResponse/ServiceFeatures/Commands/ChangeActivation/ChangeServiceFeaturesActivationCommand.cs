using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.ChangeActivation;

public sealed class ChangeServiceFeaturesActivationCommand : ICommand, IWebRequest
{
    public List<long> ServiceFeaturesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/ServiceFeature/ChangeServiceFeaturesActivation";
}
