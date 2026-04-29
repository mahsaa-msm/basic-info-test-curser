using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;

public sealed record CreateVehicleColorWithTenantIdParameter(long TenantId,
                                                              DIPTitle Title,
                                                              NullableTitle DisplayTitle,
                                                              CoreId CoreId,
                                                              ColorHash ColorHash,
                                                              Common.ValueObjects.Priority Priority,
                                                              BusinessId? TenantKey);



