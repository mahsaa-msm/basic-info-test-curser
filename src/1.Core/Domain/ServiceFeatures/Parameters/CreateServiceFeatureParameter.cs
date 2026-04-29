using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.ServiceFeatures.Parameters;

public sealed record CreateServiceFeatureParameter(ServiceFeatureCategory Key,
                                                   bool IsIssuable,
                                                   bool CanViewHistory,
                                                   NullableCoreId InsuranceTypeCoreId,
                                                   Description? Description);



