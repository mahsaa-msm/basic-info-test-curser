using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.InsuranceUnits.Parameters;

public sealed record CreateInsuranceUnitWithTenantIdParameter(long TenantId,
                                                              DIPTitle Name,
                                                              DIPTitle Title,
                                                              NullableTitle DisplayTitle,
                                                              CoreId CoreId,
                                                              CoreId CityCoreId,
                                                              Code Code,
                                                              GeoCoordinate? Location,
                                                              InsuranceUnitType Type,
                                                              InsuranceUnitState State,
                                                              Common.ValueObjects.Priority Priority,
                                                              BusinessId? TenantKey);
