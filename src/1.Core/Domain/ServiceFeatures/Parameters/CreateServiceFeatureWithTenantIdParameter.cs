using Master.Data.Core.Resources;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record CreateServiceFeatureWithTenantIdParameter(long TenantId,
                                                               ServiceFeatureCategory Key);
