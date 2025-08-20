using Master.Data.Core.RequestResponse.Countries.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.Countries.Commands.Create;
using Master.Data.Core.RequestResponse.Countries.Commands.Delete;
using Master.Data.Core.RequestResponse.Countries.Commands.Update;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Countries.Queries.GetById;
using Master.Data.Endpoints.API.Countries.Queries.GetActiveCountries;
using Master.Data.Endpoints.API.Countries.Queries.GetAllCountries;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Countries;

[Route("api/[controller]")]
[ValidateTenantHeader]
public class CountryController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand command)
        => await Create<CreateCountryCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeCountriesActivation([FromBody] ChangeCountriesActivationCommand commnad)
        => await Edit(commnad);

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteCountry([FromBody] DeleteCountryCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCountryById([FromQuery] GetCountryByIdQuery query)
        => await Query<GetCountryByIdQuery, CountryQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCountriesPagedFilter([FromQuery] GetAllCountriesPagedFilterQuery query)
        => await Query<GetAllCountriesPagedFilterQuery, PagedData<CountryListItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCountries([FromQuery] GetAllCountriesViewModel viewModel)
        => await Query<GetAllCountriesQuery, List<CountrySelectItemQr>>(viewModel.GetQuery());

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetActiveCountries([FromQuery] GetActiveCountriesViewModel viewModel)
        => await Query<GetAllCountriesQuery, List<CountrySelectItemQr>>(viewModel.GetQuery());
    #endregion
}
