using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.Update;

public sealed class UpdateAgreementObligationCommand : ICommand, IWebRequest
{
    public long AgreementObligationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string AgreementCoreId { get; set; } = string.Empty;
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public double? PrepaymentPercentage { get; set; }
    public int? FirstInstallmentDeadline { get; set; }
    public int? InstallmentsCount { get; set; }
    public int? InstallmentInterval { get; set; }
    public string AgreementObligationNumber { get; set; } = string.Empty;
    public string AgreementNumber { get; set; } = string.Empty;
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
    public SalesType SalesType { get; set; }
    public long Priority { get; set; }

    public string Path => "/Api/AgreementObligation/UpdateAgreementObligation";
}

