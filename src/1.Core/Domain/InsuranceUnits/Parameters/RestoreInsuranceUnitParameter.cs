using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.InsuranceUnits.Parameters;

public sealed record RestoreInsuranceUnitParameter(DIPTitle Name,
                                                   DIPTitle Title,
                                                   NullableTitle DisplayTitle,
                                                   CoreId CityCoreId,
                                                   Code Code,
                                                   GeoCoordinate? Location,
                                                   InsuranceUnitType Type,
                                                   InsuranceUnitState State,
                                                   Common.ValueObjects.Priority Priority);

