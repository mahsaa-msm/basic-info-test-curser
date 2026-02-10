using Master.Data.Core.RequestResponse.CoreInsuranceApis.Common;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;

public sealed class GetAllAgreementObligationsResponse : BaseCoreInsuranceResponse
{
    [JsonProperty("movafeghatNamehID")]
    [JsonPropertyName("movafeghatNamehID")]
    public long AgreementCoreId { get; set; }

    [JsonProperty("taahodatMovafeghatNamehID")]
    [JsonPropertyName("taahodatMovafeghatNamehID")]
    public long AgreementObligationCoreId { get; set; }

    [JsonProperty("naam")]
    [JsonPropertyName("naam")]
    public string Title { get; set; } = default!;

    [JsonProperty("code")]
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonProperty("tarikhShoroo")]
    [JsonPropertyName("tarikhShoroo")]
    public long? StartDate { get; set; }

    [JsonProperty("tarikhPayan")]
    [JsonPropertyName("tarikhPayan")]
    public long? EndDate { get; set; }

    [JsonProperty("darsadPishPardakht")]
    [JsonPropertyName("darsadPishPardakht")]
    public double? PrepaymentPercentage { get; set; }

    [JsonProperty("mohlatGhestAvvalRooz")]
    [JsonPropertyName("mohlatGhestAvvalRooz")]
    public int? FirstInstallmentDeadline { get; set; }

    [JsonProperty("tedadAghsaat")]
    [JsonPropertyName("tedadAghsaat")]
    public int? InstallmentsCount { get; set; }

    [JsonProperty("bazehAghsaatMaah")]
    [JsonPropertyName("bazehAghsaatMaah")]
    public int? InstallmentInterval { get; set; }

    [JsonProperty("shomarehTaahodatMN")]
    [JsonPropertyName("shomarehTaahodatMN")]
    public string AgreementObligationNumber { get; set; } = default!;

    [JsonProperty("shomarehMovafeghatNameh")]
    [JsonPropertyName("shomarehMovafeghatNameh")]
    public string AgreementNumber { get; set; } = default!;

    [JsonProperty("noeBimehID")]
    [JsonPropertyName("noeBimehID")]
    public long InsuranceTypeCoreId { get; set; }

    [JsonProperty("noeForoosh")]
    [JsonPropertyName("noeForoosh")]
    public SalesType SalesType { get; set; }

    [JsonProperty("tarhSodoorList")]
    [JsonPropertyName("tarhSodoorList")]
    public List<IssuanceSchemeResponse> IssuanceSchemes { get; set; } = default!;
}

public sealed class IssuanceSchemeResponse : BaseCoreInsuranceResponse
{
    [JsonProperty("tarhSodoorID")]
    [JsonPropertyName("tarhSodoorID")]
    public long IssuanceSchemeCoreId { get; set; }

    [JsonProperty("noeBimehID")]
    [JsonPropertyName("noeBimehID")]
    public long InsuranceTypeCoreId { get; set; }

    [JsonProperty("naam")]
    [JsonPropertyName("naam")]
    public string Title { get; set; } = default!;

    [JsonProperty("code")]
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonProperty("faal")]
    [JsonPropertyName("faal")]
    public bool IsActive { get; set; }

    [JsonProperty("azTarikhSodoor")]
    [JsonPropertyName("azTarikhSodoor")]
    public long? IssuanceDateFrom { get; set; }

    [JsonProperty("taTarikhSodoor")]
    [JsonPropertyName("taTarikhSodoor")]
    public long? IssuanceDateTo { get; set; }

    [JsonProperty("tarikhShorooAz")]
    [JsonPropertyName("tarikhShorooAz")]
    public long? StartDateFrom { get; set; }

    [JsonProperty("tarikhShorooTa")]
    [JsonPropertyName("tarikhShorooTa")]
    public long? StartDateTo { get; set; }

    [JsonProperty("takhfifEzafeh")]
    [JsonPropertyName("takhfifEzafeh")]
    public double? AdditionalDiscount { get; set; }

    [JsonProperty("darsadTakhfifEzafeh")]
    [JsonPropertyName("darsadTakhfifEzafeh")]
    public double? AdditionalDiscountPercentage { get; set; }

    [JsonProperty("olaviat")]
    [JsonPropertyName("olaviat")]
    public int Priority { get; set; }
}
