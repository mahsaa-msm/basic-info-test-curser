using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;

public sealed record CreateVehicleColorParameter(DIPTitle Title,
                                                  NullableTitle DisplayTitle,
                                                  CoreId CoreId,
                                                  ColorHash ColorHash,
                                                  Common.ValueObjects.Priority Priority);



