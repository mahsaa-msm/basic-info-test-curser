using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record CreateInsuranceTypeWithTenantIdParameter(long TenantId,
                                                        Title Title,
                                                        NullableTitle DisplayTitle,
                                                        CoreId CoreId,
                                                        Code Code,
                                                        Common.ValueObjects.Priority Priority,
                                                        BusinessId? TenantKey);