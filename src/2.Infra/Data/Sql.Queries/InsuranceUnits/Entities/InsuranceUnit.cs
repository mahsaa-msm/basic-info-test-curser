using Master.Data.Infra.Data.Sql.Queries.Cities.Entities;
using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using NetTopologySuite.Geometries;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceUnits.Entities;

public sealed class InsuranceUnit : BaseTenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Point? Location { get; set; }
    public InsuranceUnitType Type { get; set; }
    public InsuranceUnitState State { get; set; }
    public long Priority { get; set; }
    public string CityCoreId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public City City { get; set; } = null!;
}
