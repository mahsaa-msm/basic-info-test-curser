using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.VehicleColors;

[Route("api/[controller]")]
[Tags("VehicleColor - (انواع بیمه)")]
[ValidateTenantHeader]
public class VehicleColorController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateDIPVehicleColor([FromBody] CreateVehicleColorCommand command)
        => await Create<CreateVehicleColorCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateDIPVehicleColor([FromBody] UpdateVehicleColorCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeDIPVehicleColorsActivation([FromBody] ChangeVehicleColorsActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchVehicleColorsFromSource()
        => await Edit(new FetchVehicleColorsFromSourceCommand());

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDIPVehicleColors([FromQuery] GetAllVehicleColorQuery query)
    => await Query<GetAllVehicleColorQuery, List<VehicleColorSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetDIPVehicleColorById([FromQuery] GetVehicleColorByIdQuery query)
        => await Query<GetVehicleColorByIdQuery, VehicleColorQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDIPVehicleColorsPagedFilter([FromQuery] GetAllVehicleColorsPagedFilterQuery query)
        => await Query<GetAllVehicleColorsPagedFilterQuery, PagedData<VehicleColorListItemQr>>(query);

    #endregion
}


