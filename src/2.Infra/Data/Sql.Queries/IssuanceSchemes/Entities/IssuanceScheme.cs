using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.IssuanceSchemes.Entities;
public sealed class IssuanceScheme : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public DateTime? FromStartDateUtc { get; private set; }
    public DateTime? ToStartDateUtc { get; private set; }
    public DateTime? FromIssueDateUtc { get; private set; }
    public DateTime? ToIssueDateUtc { get; private set; }
    public string? InsuranceTypeCoreId { get; private set; }
    public AdjustmentType? AdjustmentType { get; private set; }
    public double? AdjustmentPercent { get; private set; }
}

