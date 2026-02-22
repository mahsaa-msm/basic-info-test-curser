using Master.Data.Core.RequestResponse.Countries.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.Countries.Commands.Create;
using Master.Data.Core.RequestResponse.Countries.Commands.Delete;
using Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
using Master.Data.Core.RequestResponse.Countries.Commands.Update;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Countries.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.Countries;

[Route("api/[controller]")]
[Tags("Country - (کشور ها)")]
[ValidateTenantHeader]
public class CountryController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateDIPCountry([FromBody] CreateCountryCommand command)
        => await Create<CreateCountryCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateDIPCountry([FromBody] UpdateCountryCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeDIPCountriesActivation([FromBody] ChangeCountriesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchDIPCountriesFromSource()
    => await Edit(new FetchCountriesFromSourceCommand());

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteDIPCountry([FromBody] DeleteCountryCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDIPCountries([FromQuery] GetAllCountryQuery query)
    => await Query<GetAllCountryQuery, List<CountrySelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetDIPCountryById([FromQuery] GetCountryByIdQuery query)
        => await Query<GetCountryByIdQuery, CountryQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDIPCountriesPagedFilter([FromQuery] GetAllCountriesPagedFilterQuery query)
        => await Query<GetAllCountriesPagedFilterQuery, PagedData<CountryListItemQr>>(query);

    #endregion
}
