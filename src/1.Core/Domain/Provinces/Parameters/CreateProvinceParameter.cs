using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;
public sealed record CreateProvinceParameter(Title Title,
                                             NullableTitle DisplayTitle,
                                             CoreId CoreId,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority,
                                             CoreId CountryCoreId);
