using Master.Data.Core.RequestResponse.VehicleColors.Queries.CommanResults;
using Master.Data.Core.RequestResponse.VehicleColors.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.VehicleColors;

[Route("api/[controller]")]
[Tags("VehicleColor - (رنگ خودرو)")]
[ValidateTenantHeader]
public class VehicleColorController : BaseController
{
    [HttpGet("GetAllVehicleColor")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleColor([FromQuery] GetAllVehicleColorQuery query)
    {
        return Ok(new[] {
             new VehicleColorItemQr { Id= 247, Title= "سفید", },
             new VehicleColorItemQr { Id= 249, Title= "نقره ای", },
             new VehicleColorItemQr { Id= 251, Title= "مشکی", },
             new VehicleColorItemQr { Id= 300, Title= "آلبالوئی", },
        });
    }
}

