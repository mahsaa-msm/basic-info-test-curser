namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;

public sealed class AgreementObligationListItemQr
{
    public long Id { get; set; }
    public string CoreId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string DisplayTitle { get; set; } = default!;
    public string Code { get; set; } = default!;
    public bool IsActive { get; set; }
    public long Priority { get; set; }
    public string AgreementObligationNumber { get; set; } = default!;
    public string AgreementNumber { get; set; } = default!;
    public string AgreementCoreId { get; set; } = default!;
    public string InsuranceTypeCoreId { get; set; } = default!;
}

