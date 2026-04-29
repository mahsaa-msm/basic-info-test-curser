using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Provinces.Parameters;

public sealed record RestoreProvinceParameter(DIPTitle Title,
                                              NullableTitle DisplayTitle,
                                              Code Code,
                                              Common.ValueObjects.Priority Priority,
                                              CoreId CountryCoreId);

