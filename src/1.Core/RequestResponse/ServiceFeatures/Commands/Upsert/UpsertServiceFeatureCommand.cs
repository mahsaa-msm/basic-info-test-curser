using Vehicle.Insurance.Core.Domain.ServiceFeatures.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.Upsert;

public sealed class UpsertServiceFeatureCommand : ICommand, IWebRequest
{
    public ServiceFeatureCategory Key { get; set; }

    public List<long> TenantIds { get; set; } = new();
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public bool IsIssuable { get; set; }
    public bool CanViewHistory { get; set; }
    public string? InsuranceTypeCoreId { get; set; }
    public string Path => "/Api/ServiceFeature/UpsertServiceFeature";

    public CreateServiceFeatureWithTenantIdParameter ToParemeter(long tenantId) => new CreateServiceFeatureWithTenantIdParameter(tenantId, IsIssuable, CanViewHistory, InsuranceTypeCoreId, Description, Key);

}

