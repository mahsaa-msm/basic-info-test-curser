using Master.Data.Core.RequestResponse.Cities.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.Cities.Commands.Create;
using Master.Data.Core.RequestResponse.Cities.Commands.Delete;
using Master.Data.Core.RequestResponse.Cities.Commands.Fetch;
using Master.Data.Core.RequestResponse.Cities.Commands.Update;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Cities.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.Cities;

[Route("api/[controller]")]
[Tags("City - (شهر ها)")]
[ValidateTenantHeader]
public class CityController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCity([FromBody] CreateCityCommand command)
        => await Create<CreateCityCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateCity([FromBody] UpdateCityCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeCitiesActivation([FromBody] ChangeCitiesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchCitiesFromSource([FromBody] FetchCitiesFromSourceCommand commnad)
    => await Edit(commnad);

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteCity([FromBody] DeleteCityCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCities([FromQuery] GetAllCitiesQuery query)
    => await Query<GetAllCitiesQuery, List<CitySelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCityById([FromQuery] GetCityByIdQuery query)
        => await Query<GetCityByIdQuery, CityQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCitiesPagedFilter([FromQuery] GetAllCitiesPagedFilterQuery query)
        => await Query<GetAllCitiesPagedFilterQuery, PagedData<CityListItemQr>>(query);

    #endregion
}
