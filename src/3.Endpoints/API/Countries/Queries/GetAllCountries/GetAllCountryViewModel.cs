using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;

namespace Master.Data.Endpoints.API.Countries.Queries.GetAllCountries;

public class GetAllCountryViewModel
{
    public GetAllCountryQuery GetQuery()
    {
        return new GetAllCountryQuery()
        {
            IsActive = null,
        };
    }
}
