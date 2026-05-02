using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.VehicleBrands;

[Route("api/[controller]")]
[Tags("VehicleBrand - (برند وسیله نقلیه)")]
[ValidateTenantHeader]
public class VehicleBrandController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateVehicleBrand([FromBody] CreateVehicleBrandCommand command)
        => await Create<CreateVehicleBrandCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateVehicleBrand([FromBody] UpdateVehicleBrandCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeVehicleBrandsActivation(
        [FromBody] ChangeVehicleBrandsActivationCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchVehicleBrandsFromSource()
        => await Edit(new FetchVehicleBrandsFromSourceCommand());

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleBrands([FromQuery] GetAllVehicleBrandQuery query)
        => await Query<GetAllVehicleBrandQuery, List<VehicleBrandSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetVehicleBrandById([FromQuery] GetVehicleBrandByIdQuery query)
        => await Query<GetVehicleBrandByIdQuery, VehicleBrandQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllVehicleBrandsPagedFilter(
        [FromQuery] GetAllVehicleBrandsPagedFilterQuery query)
        => await Query<GetAllVehicleBrandsPagedFilterQuery, PagedData<VehicleBrandListItemQr>>(query);
}
