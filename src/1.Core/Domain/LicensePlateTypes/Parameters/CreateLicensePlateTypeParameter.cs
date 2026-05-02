using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.LicensePlateTypes.Parameters;

public sealed record CreateLicensePlateTypeParameter(DIPTitle Title,
    NullableTitle DisplayTitle,
    CoreId CoreId,
    Common.ValueObjects.Priority Priority);
