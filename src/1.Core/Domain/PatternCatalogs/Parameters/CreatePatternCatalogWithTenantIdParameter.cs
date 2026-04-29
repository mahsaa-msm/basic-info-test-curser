using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.PatternCatalogs.Parameters;

public sealed record CreatePatternCatalogWithTenantIdParameter(long TenantId,
                                                               PatternKey Key,
                                                               PatternCatalogType Type,
                                                               RegexExpression Pattern,
                                                               Common.ValueObjects.Priority Priority,
                                                               Description? Description,
                                                               BusinessId? TenantKey);
