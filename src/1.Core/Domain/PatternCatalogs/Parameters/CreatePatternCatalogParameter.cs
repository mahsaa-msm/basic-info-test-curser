using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.PatternCatalogs.Parameters;

public sealed record CreatePatternCatalogParameter(PatternKey Key,
                                                   RegexExpression Pattern,
                                                   PatternCatalogType Type,
                                                   Common.ValueObjects.Priority Priority,
                                                   Description? Description);

