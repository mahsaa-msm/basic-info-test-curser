using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Cities.Parameters;

public sealed record CreateCityWithTenantIdParameter(long TenantId,
                                                     DIPTitle Title,
                                                     NullableTitle DisplayTitle,
                                                     CoreId CoreId,
                                                     Code Code,
                                                     Common.ValueObjects.Priority Priority,
                                                     CoreId ProvinceCoreId,
                                                     BusinessId? TenantKey);

