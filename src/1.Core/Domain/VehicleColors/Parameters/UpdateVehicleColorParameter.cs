using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;

public sealed record UpdateVehicleColorParameter(DIPTitle Title,
                                                  DIPTitle DisplayTitle,
                                                  ColorHash ColorHash,
                                                  Common.ValueObjects.Priority Priority);



