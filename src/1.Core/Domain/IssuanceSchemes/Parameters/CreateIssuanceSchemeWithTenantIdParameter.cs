using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.IssuanceSchemes.Parameters;

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