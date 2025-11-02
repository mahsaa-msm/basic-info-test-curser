using Master.Data.Core.RequestResponse.VehicleTips.Queries.CommonResults;
using Master.Data.Core.RequestResponse.VehicleTips.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.VehicleTips;

[Route("api/[controller]")]
[Tags("VehicleTip - (تیپ وسیله نقلیه)")]
[ValidateTenantHeader]
public class VehicleTipController : BaseController
{
    [HttpGet("GetAllVehicleTip")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleTip([FromQuery] GetAllVehicleTipQuery query)
    {
        return Ok(new[] {
             new VehicleTipItemQr { Id= 25151, Title= "130", },
             new VehicleTipItemQr { Id= 25152, Title= "تیبا 2", },
             new VehicleTipItemQr { Id= 25174, Title= "النترا", },
             new VehicleTipItemQr { Id= 25187, Title= "1600", },
             new VehicleTipItemQr { Id= 25230, Title= "کوپهSLK55AMG", },
        });
    }
}

