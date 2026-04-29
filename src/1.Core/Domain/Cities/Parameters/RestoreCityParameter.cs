using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Cities.Parameters;

public sealed record RestoreCityParameter(DIPTitle Title,
                                          NullableTitle DisplayTitle,
                                          Code Code,
                                          Common.ValueObjects.Priority Priority,
                                          CoreId ProvinceCoreId);

