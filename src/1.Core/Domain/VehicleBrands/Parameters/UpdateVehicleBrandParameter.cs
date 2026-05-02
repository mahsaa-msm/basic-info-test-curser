using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleBrands.Parameters;

public sealed record UpdateVehicleBrandParameter(DIPTitle Title,
    DIPTitle DisplayTitle,
    Common.ValueObjects.Priority Priority);
