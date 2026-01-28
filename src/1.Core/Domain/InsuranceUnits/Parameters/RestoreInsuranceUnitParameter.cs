using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.InsuranceUnits.Parameters;

public sealed record RestoreInsuranceUnitParameter(DIPTitle Name,
                                                   DIPTitle Title,
                                                   NullableTitle DisplayTitle,
                                                   CoreId CityCoreId,
                                                   Code Code,
                                                   GeoCoordinate? Location,
                                                   InsuranceUnitType Type,
                                                   InsuranceUnitState State,
                                                   Common.ValueObjects.Priority Priority);
