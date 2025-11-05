using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Entities;

public sealed record CreateCountryWithTenantIdParameter(long TenantId,
                                                        Title Title,
                                                        NullableTitle DisplayTitle,
                                                        CoreId CoreId,
                                                        Code Code,
                                                        Common.ValueObjects.Priority Priority,
                                                        BusinessId? TenantKey);