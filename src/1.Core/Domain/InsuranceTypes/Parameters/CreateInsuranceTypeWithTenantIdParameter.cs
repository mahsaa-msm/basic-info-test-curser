using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.InsuranceTypes.Parameters;

public sealed record CreateInsuranceTypeWithTenantIdParameter(long TenantId,
                                                              DIPTitle Title,
                                                              NullableTitle DisplayTitle,
                                                              CoreId CoreId,
                                                              Code Code,
                                                              ServiceFeatureCategory? ServiceFeatureCategory,
                                                              Common.ValueObjects.Priority Priority,
                                                              BusinessId? TenantKey);
