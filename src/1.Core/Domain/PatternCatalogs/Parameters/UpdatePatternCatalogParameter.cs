using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.PatternCatalogs.Parameters;

public sealed record UpdatePatternCatalogParameter(RegexExpression Pattern,
                                                   Common.ValueObjects.Priority Priority,
                                                   Description? Description);