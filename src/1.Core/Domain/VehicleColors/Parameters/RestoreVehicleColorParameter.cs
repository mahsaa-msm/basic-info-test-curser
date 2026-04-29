using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;

public sealed record RestoreVehicleColorParameter(DIPTitle Title,
                                                   NullableTitle DisplayTitle,
                                                   ColorHash ColorHash,
                                                   Common.ValueObjects.Priority Priority);




