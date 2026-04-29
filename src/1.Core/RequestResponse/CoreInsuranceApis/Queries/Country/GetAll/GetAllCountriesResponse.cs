using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.CommonResults;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;

public sealed class GetAllCountriesResponse
{
    public List<BaseCoreInsuraceSelectItemQr> itemList { get; set; } = new();
}

