namespace Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;

public sealed class AgreementObligationSelectItemQr
{
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string AgreementObligationNumber { get; set; } = string.Empty;
    public string AgreementCoreId { get; set; } = string.Empty;
    public string AgreementNumber { get; set; } = string.Empty;
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
}
