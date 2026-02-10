using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;

public sealed record UpdateProvinceParameter(DIPTitle Title,
                                             DIPTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);
