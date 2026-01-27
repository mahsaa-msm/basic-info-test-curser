using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.PatternCatalogs.Parameters;

public sealed record UpdatePatternCatalogParameter(RegexExpression Pattern,
                                                   PatternCatalogType Type,
                                                   Common.ValueObjects.Priority Priority,
                                                   Description? Description);