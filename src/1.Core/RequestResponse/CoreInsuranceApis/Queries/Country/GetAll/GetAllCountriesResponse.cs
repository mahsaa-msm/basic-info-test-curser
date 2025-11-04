using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.CommonResults;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;
public sealed class GetAllCountriesResponse : BaseListItemResponse
{
    public long id { get; set; }
    public string title { get; set; } = string.Empty;
    public string? centInsurCode { get; set; }
}
