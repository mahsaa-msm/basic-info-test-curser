using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleBrands.Parameters;

public sealed record CreateVehicleBrandParameter(DIPTitle Title,
    NullableTitle DisplayTitle,
    CoreId CoreId,
    Common.ValueObjects.Priority Priority);
