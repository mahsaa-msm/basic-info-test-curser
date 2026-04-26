using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Update;

public sealed class UpdateServiceFeatureCommand : ICommand, IWebRequest
{
    public long ServiceFeatureId { get; set; }
    public string? Description { get; set; }
    public bool IsIssuable { get; set; }
    public bool CanViewHistory { get; set; }
    public string? InsuranceTypeCoreId { get; set; }

    public string Path => "/Api/ServiceFeature/UpdateServiceFeature";

    public UpdateServiceFeatureParameter ToParemeter() => new UpdateServiceFeatureParameter(IsIssuable, CanViewHistory, InsuranceTypeCoreId, Description);
}