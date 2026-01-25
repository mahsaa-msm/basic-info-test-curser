using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Provinces.Parameters;

public sealed record CreateProvinceWithTenantIdParameter(long TenantId,
                                                         DIPTitle Title,
                                                         NullableTitle DisplayTitle,
                                                         CoreId CoreId,
                                                         Code Code,
                                                         Common.ValueObjects.Priority Priority,
                                                         CoreId CountryCoreId,
                                                         BusinessId? TenantKey);
