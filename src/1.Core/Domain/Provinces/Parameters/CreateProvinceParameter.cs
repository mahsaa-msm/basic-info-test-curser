using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Provinces.Parameters;

public sealed record CreateProvinceParameter(DIPTitle Title,
                                             NullableTitle DisplayTitle,
                                             CoreId CoreId,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);

