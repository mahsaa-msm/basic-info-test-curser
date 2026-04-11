using Master.Data.Core.Resources;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record CreateServiceFeatureParameter(ServiceFeatureCategory Key,
                                                   Description? Description);