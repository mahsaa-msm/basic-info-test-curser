using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;

public sealed class GetAllAgreementObligationsRequest : IWebRequest
{
    [JsonProperty("shomarehTaahodatMN")]
    [JsonPropertyName("shomarehTaahodatMN")]
    public string? AgreementObligationNumber { get; set; }

    [JsonProperty("shomarehMovafeghatNameh")]
    [JsonPropertyName("shomarehMovafeghatNameh")]
    public string? AgreementNumber { get; set; }

    [JsonProperty("codeTarh")]
    [JsonPropertyName("codeTarh")]
    public string? IssuanceSchemeCode { get; set; }

    public string Path => "/taahodatMovafeghatNameh/findByFilter";
}

