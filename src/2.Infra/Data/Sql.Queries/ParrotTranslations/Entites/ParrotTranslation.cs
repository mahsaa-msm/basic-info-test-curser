using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.ParrotTranslations.Entites;

public sealed class ParrotTranslation : BaseTenantEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Culture { get; set; }
}

