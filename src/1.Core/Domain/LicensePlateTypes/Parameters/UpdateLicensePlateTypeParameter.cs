using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.LicensePlateTypes.Parameters;

public sealed record UpdateLicensePlateTypeParameter(DIPTitle Title,
    DIPTitle DisplayTitle,
    Common.ValueObjects.Priority Priority);
