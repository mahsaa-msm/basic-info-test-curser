using Vehicle.Insurance.Core.Domain.ServiceFeatures.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.Create;

public sealed class CreateServiceFeatureCommand : ICommand<long>, IWebRequest
{
    public ServiceFeatureCategory Key { get; set; }
    public string? Description { get; set; }

    public bool IsIssuable { get; set; }
    public bool CanViewHistory { get; set; }
    public string? InsuranceTypeCoreId { get; set; }

    public string Path => "/Api/ServiceFeature/CreateServiceFeature";

    public CreateServiceFeatureParameter ToParemeter() => new CreateServiceFeatureParameter(Key, IsIssuable, CanViewHistory, InsuranceTypeCoreId, Description);
}
