using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.PatternCatalogs.Parameters;

public sealed record CreatePatternCatalogWithTenantIdParameter(long TenantId,
                                                               PatternKey Key,
                                                               PatternCatalogType Type,
                                                               RegexExpression Pattern,
                                                               Common.ValueObjects.Priority Priority,
                                                               Description? Description,
                                                               BusinessId? TenantKey);