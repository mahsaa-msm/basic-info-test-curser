using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.ServiceFeatures.Parameters;

public sealed record UpdateServiceFeatureParameter(bool IsIssuable,
                                                   bool CanViewHistory,
                                                   NullableCoreId InsuranceTypeCoreId,
                                                   Description? Description);
