using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.Cities;

[Route("api/[controller]")]
[Tags("City - (شهر ها)")]
[ValidateTenantHeader]
public class CityController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateDIPCity([FromBody] CreateCityCommand command)
        => await Create<CreateCityCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateDIPCity([FromBody] UpdateCityCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeDIPCitiesActivation([FromBody] ChangeCitiesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchDIPCitiesFromSource()
    => await Edit(new FetchCitiesFromSourceCommand());

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteDIPCity([FromBody] DeleteCityCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDIPCities([FromQuery] GetAllCitiesQuery query)
    => await Query<GetAllCitiesQuery, List<CitySelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetDIPCityById([FromQuery] GetCityByIdQuery query)
        => await Query<GetCityByIdQuery, CityQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDIPCitiesPagedFilter([FromQuery] GetAllCitiesPagedFilterQuery query)
        => await Query<GetAllCitiesPagedFilterQuery, PagedData<CityListItemQr>>(query);

    #endregion
}

