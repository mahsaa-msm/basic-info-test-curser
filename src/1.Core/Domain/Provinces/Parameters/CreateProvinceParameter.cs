using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;

public sealed record CreateProvinceParameter(DIPTitle Title,
                                             NullableTitle DisplayTitle,
                                             CoreId CoreId,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);
