using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.ServiceFeatures.Parameters;

public sealed record UpdateServiceFeatureParameter(bool IsIssuable,
                                                   bool CanViewHistory,
                                                   NullableCoreId InsuranceTypeCoreId,
                                                   Description? Description);

