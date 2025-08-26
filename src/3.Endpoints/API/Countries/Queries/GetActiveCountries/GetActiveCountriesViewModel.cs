using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;

namespace Master.Data.Endpoints.API.Countries.Queries.GetActiveCountries;

public class GetActiveCountriesViewModel
{
    public GetAllCountryQuery GetQuery()
    {
        return new GetAllCountryQuery()
        {
            IsActive = true,
        };
    }
}
