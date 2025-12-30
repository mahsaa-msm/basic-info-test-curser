using Master.Data.Core.RequestResponse.VehicleBrands.Queries.CommonResults;
using Master.Data.Core.RequestResponse.VehicleBrands.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.VehicleBrands;

[Route("api/[controller]")]
[Tags("VehicleBrand - (برند وسیله نقلیه)")]
[ValidateTenantHeader]
public class VehicleBrandController : BaseController
{
    [HttpGet("GetAllVehicleBrand")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllVehicleBrand([FromQuery] GetAllVehicleBrandQuery query)
    {
        return Ok(new[] {
             new VehicleBrandItemQr { Id= 20244, Title= "جت رو",  },
             new VehicleBrandItemQr { Id= 20245, Title= "سایپا", },
             new VehicleBrandItemQr { Id= 20246, Title= "فولکس", },
             new VehicleBrandItemQr { Id= 20247, Title= "بی ام و", },
             new VehicleBrandItemQr { Id= 20249, Title= "ولوو", },
        });
    }
}

