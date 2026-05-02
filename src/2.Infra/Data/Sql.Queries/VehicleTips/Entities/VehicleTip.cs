using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleTips.Entities;

public sealed class VehicleTip : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string BrandCoreId { get; set; } = string.Empty;
    public string VehicleTypeCoreId { get; set; } = string.Empty;
    public string VehicleSystemCoreId { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
