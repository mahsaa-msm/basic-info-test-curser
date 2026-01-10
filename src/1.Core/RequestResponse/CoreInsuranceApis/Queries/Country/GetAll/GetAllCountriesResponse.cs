using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.CommonResults;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;
public sealed class GetAllCountriesResponse
{
    public List<BaseCoreInsuraceSelectItemQr> itemList { get; set; } = new();
}
