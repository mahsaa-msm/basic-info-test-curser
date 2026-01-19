using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Entities;

public sealed class AgreementObligation : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public double? PrepaymentPercentage { get; set; }
    public int? FirstInstallmentDeadline { get; set; }
    public int? InstallmentsCount { get; set; }
    public int? InstallmentInterval { get; set; }
    public string AgreementObligationNumber { get; set; } = string.Empty;
    public string AgreementNumber { get; set; } = string.Empty;
    public SalesType SalesType { get; set; }
    public string AgreementCoreId { get; set; } = string.Empty;
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }


    public HashSet<string> IssuanceSchemeCoreIds = new();
}