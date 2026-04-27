using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record CreateServiceFeatureParameter(ServiceFeatureCategory Key,
                                                   bool IsIssuable,
                                                   bool CanViewHistory,
                                                   NullableCoreId InsuranceTypeCoreId,
                                                   Description? Description);


