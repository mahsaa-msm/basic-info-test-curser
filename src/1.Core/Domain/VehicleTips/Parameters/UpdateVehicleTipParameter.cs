using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;

public sealed record UpdateVehicleTipParameter(DIPTitle Title,
    DIPTitle DisplayTitle,
    CoreId BrandCoreId,
    CoreId VehicleTypeCoreId,
    CoreId VehicleSystemCoreId,
    Common.ValueObjects.Priority Priority);
