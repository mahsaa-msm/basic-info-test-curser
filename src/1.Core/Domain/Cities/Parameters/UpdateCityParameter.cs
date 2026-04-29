using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Cities.Parameters;

public sealed record UpdateCityParameter(DIPTitle Title,
                                         DIPTitle DisplayTitle,
                                         Code Code,
                                         Common.ValueObjects.Priority Priority,
                                         CoreId ProvinceCoreId);

