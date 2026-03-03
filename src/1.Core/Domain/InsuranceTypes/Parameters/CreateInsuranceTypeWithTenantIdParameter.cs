using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record CreateInsuranceTypeWithTenantIdParameter(long TenantId,
                                                              DIPTitle Title,
                                                              NullableTitle DisplayTitle,
                                                              CoreId CoreId,
                                                              Code Code,
                                                              ServiceFeatureCategory? ServiceFeatureCategory,
                                                              Common.ValueObjects.Priority Priority,
                                                              BusinessId? TenantKey);