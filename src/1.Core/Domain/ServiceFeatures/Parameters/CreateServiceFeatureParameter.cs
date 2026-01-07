using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record CreateServiceFeatureParameter(ServiceFeatureKey Key,
                                                   Name ServiceName,
                                                   Name FeatureName,
                                                   Description? Description);