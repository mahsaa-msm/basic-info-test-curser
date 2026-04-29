using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;

public sealed record CreateIssuanceSchemeWithTenantIdParameter(long TenantId,
                                                               DIPTitle Title,
                                                               NullableTitle DisplayTitle,
                                                               CoreId CoreId,
                                                               Code Code,
                                                               DateTime? FromStartDateUtc,
                                                               DateTime? ToStartDateUtc,
                                                               DateTime? FromIssueDateUtc,
                                                               DateTime? ToIssueDateUtc,
                                                               CoreId InsuranceTypeCoreId,
                                                               AdjustmentType? AdjustmentType,
                                                               NullablePercentage? AdjustmentPercent,
                                                               IsActive IsActive,
                                                               Common.ValueObjects.Priority Priority,
                                                               BusinessId? TenantKey);
