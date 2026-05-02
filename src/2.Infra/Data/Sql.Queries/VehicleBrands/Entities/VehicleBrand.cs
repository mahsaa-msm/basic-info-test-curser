using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleBrands.Entities;

public sealed class VehicleBrand : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
