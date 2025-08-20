using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;

namespace Master.Data.Endpoints.API.Countries.Queries.GetAllCountries;

public class GetAllCountriesViewModel
{
    public GetAllCountriesQuery GetQuery()
    {
        return new GetAllCountriesQuery()
        {
            IsActive = null,
        };
    }
}
