using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Provinces.Parameters;

public sealed record UpdateProvinceParameter(DIPTitle Title,
                                             DIPTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);

