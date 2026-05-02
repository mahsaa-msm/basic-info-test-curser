using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;

public sealed record CreateVehicleTipParameter(DIPTitle Title,
    NullableTitle DisplayTitle,
    CoreId CoreId,
    CoreId BrandCoreId,
    CoreId VehicleTypeCoreId,
    CoreId VehicleSystemCoreId,
    Common.ValueObjects.Priority Priority);
