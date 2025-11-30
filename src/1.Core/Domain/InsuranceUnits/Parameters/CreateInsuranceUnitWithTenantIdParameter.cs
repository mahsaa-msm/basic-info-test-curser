using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.InsuranceUnits.Parameters;
public sealed record CreateInsuranceUnitWithTenantIdParameter(long TenantId,
                                                              Title Name,
                                                              Title Title,
                                                              NullableTitle DisplayTitle,
                                                              CoreId CoreId,
                                                              CoreId CityCoreId,
                                                              Code Code,
                                                              GeoCoordinate? Location,
                                                              InsuranceUnitType Type,
                                                              InsuranceUnitState State,
                                                              Common.ValueObjects.Priority Priority,
                                                              BusinessId? TenantKey);