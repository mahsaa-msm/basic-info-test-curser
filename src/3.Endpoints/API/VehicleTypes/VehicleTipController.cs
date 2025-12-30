using Master.Data.Core.RequestResponse.VehicleTypes.Queries.CommonResults;
using Master.Data.Core.RequestResponse.VehicleTypes.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.VehicleTypes;

[Route("api/[controller]")]
[Tags("VehicleType - (نوع وسیله نقلیه)")]
[ValidateTenantHeader]
public class VehicleTypeController : BaseController
{
    [HttpGet("GetAllVehicleType")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleType([FromQuery] GetAllVehicleTypeQuery query)
    {
        return Ok(new[] {
             new VehicleTypeItemQr { Id= 19, Title= "سواری", },
             new VehicleTypeItemQr { Id= 28, Title= "موتور سیکلت", }
        });
    }
}

