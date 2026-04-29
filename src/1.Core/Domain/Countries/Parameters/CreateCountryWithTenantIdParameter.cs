using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Countries.Parameters;

public sealed record CreateCountryWithTenantIdParameter(long TenantId,
                                                        DIPTitle Title,
                                                        NullableTitle DisplayTitle,
                                                        CoreId CoreId,
                                                        Code Code,
                                                        Common.ValueObjects.Priority Priority,
                                                        BusinessId? TenantKey);
