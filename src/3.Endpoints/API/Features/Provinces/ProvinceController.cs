using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.Provinces;


[Route("api/[controller]")]
[Tags("Province - (استان ها)")]
[ValidateTenantHeader]
public class ProvinceController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateProvince([FromBody] CreateProvinceCommand command)
        => await Create<CreateProvinceCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateProvince([FromBody] UpdateProvinceCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeProvincesActivation([FromBody] ChangeProvincesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchProvincesFromSource()
    => await Edit(new FetchProvincesFromSourceCommand());

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteProvince([FromBody] DeleteProvinceCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllProvinces([FromQuery] GetAllProvincesQuery query)
    => await Query<GetAllProvincesQuery, List<ProvinceSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProvinceById([FromQuery] GetProvinceByIdQuery query)
        => await Query<GetProvinceByIdQuery, ProvinceQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllProvincesPagedFilter([FromQuery] GetAllProvincesPagedFilterQuery query)
        => await Query<GetAllProvincesPagedFilterQuery, PagedData<ProvinceListItemQr>>(query);

    #endregion
}

