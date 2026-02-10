using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;

public sealed class AgreementObligationQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string AgreementCoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public double? PrepaymentPercentage { get; set; }
    public int? FirstInstallmentDeadline { get; set; }
    public int? InstallmentsCount { get; set; }
    public int? InstallmentInterval { get; set; }
    public string AgreementObligatoinNumber { get; set; } = string.Empty;
    public string AgreementNumber { get; set; } = string.Empty;
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
    public SalesType SalesType { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public bool IsEditable { get; set; }
}