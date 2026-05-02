using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.VehicleTips;

[Route("api/[controller]")]
[Tags("VehicleTip - (تیپ وسیله نقلیه)")]
[ValidateTenantHeader]
public class VehicleTipController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateVehicleTip([FromBody] CreateVehicleTipCommand command)
        => await Create<CreateVehicleTipCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateVehicleTip([FromBody] UpdateVehicleTipCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeVehicleTipsActivation([FromBody] ChangeVehicleTipsActivationCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchVehicleTipsFromSource()
        => await Edit(new FetchVehicleTipsFromSourceCommand());

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleTips([FromQuery] GetAllVehicleTipQuery query)
        => await Query<GetAllVehicleTipQuery, List<VehicleTipSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetVehicleTipById([FromQuery] GetVehicleTipByIdQuery query)
        => await Query<GetVehicleTipByIdQuery, VehicleTipQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllVehicleTipsPagedFilter([FromQuery] GetAllVehicleTipsPagedFilterQuery query)
        => await Query<GetAllVehicleTipsPagedFilterQuery, PagedData<VehicleTipListItemQr>>(query);
}
