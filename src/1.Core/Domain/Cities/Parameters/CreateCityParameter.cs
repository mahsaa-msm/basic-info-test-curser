using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Cities.Parameters;

public sealed record CreateCityParameter(DIPTitle Title,
                                         NullableTitle DisplayTitle,
                                         CoreId CoreId,
                                         Code Code,
                                         Common.ValueObjects.Priority Priority,
                                         CoreId ProvinceCoreId);

