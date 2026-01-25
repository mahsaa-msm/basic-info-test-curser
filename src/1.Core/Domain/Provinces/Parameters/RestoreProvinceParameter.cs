using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;

public sealed record RestoreProvinceParameter(DIPTitle Title,
                                              NullableTitle DisplayTitle,
                                              Code Code,
                                              Common.ValueObjects.Priority Priority,
                                              CoreId CountryCoreId);
