using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;
public sealed record RestoreProvinceParameter(Title Title,
                                              NullableTitle DisplayTitle,
                                              Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);
