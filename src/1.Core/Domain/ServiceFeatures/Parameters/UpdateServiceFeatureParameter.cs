using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record UpdateServiceFeatureParameter(Name ServiceName,
                                                   Name FeatureName,
                                                   Description? Description);
