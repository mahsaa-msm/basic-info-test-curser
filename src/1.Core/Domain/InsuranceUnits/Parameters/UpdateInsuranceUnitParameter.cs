using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.InsuranceUnits.Parameters;
public sealed record UpdateInsuranceUnitParameter(Title Name,
                                                  Title Title,
                                                  Title DisplayTitle,
                                                  CoreId CityCoreId,
                                                  Code Code,
                                                  GeoCoordinate? Location,
                                                  InsuranceUnitType Type,
                                                  InsuranceUnitState State,
                                                  Common.ValueObjects.Priority Priority);
