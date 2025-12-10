using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Master.Data.Core.Domain.PatternCatalogs.Parameters;

public sealed record CreatePatternCatalogWithTenantIdParameter(long TenantId,
                                                               PatternKey Key,
                                                               RegexExpression Pattern,
                                                               Common.ValueObjects.Priority Priority,
                                                               Description? Description,
                                                               BusinessId? TenantKey);