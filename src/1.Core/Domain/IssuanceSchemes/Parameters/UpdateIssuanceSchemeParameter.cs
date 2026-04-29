using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;
public sealed record UpdateIssuanceSchemeParameter(DIPTitle Title,
                                                   DIPTitle DisplayTitle,
                                                   Code Code,
                                                   DateTime? FromStartDateUtc,
                                                   DateTime? ToStartDateUtc,
                                                   DateTime? FromIssueDateUtc,
                                                   DateTime? ToIssueDateUtc,
                                                   CoreId InsuranceTypeCoreId,
                                                   AdjustmentType? AdjustmentType,
                                                   NullablePercentage? AdjustmentPercent,
                                                   Common.ValueObjects.Priority Priority);
