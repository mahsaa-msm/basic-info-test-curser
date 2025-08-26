using Master.Data.Core.RequestResponse.Cities.Queries.CommanResults;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.TravelDuration;

[Route("api/[controller]")]
[Tags("TravelDuration - (انوع پلاک)")]
[ValidateTenantHeader]
public class TravelDurationController : BaseController
{
    [HttpGet("GetAllTravelDuration")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllTravelDuration([FromQuery] GetAllTravelDurationQuery query)
    {
        return Ok(new[] { new TravelDurationItemQr { Title = "30 روز", Id = 1 }, new TravelDurationItemQr { Title = "3 روز", Id = 2 }, new TravelDurationItemQr { Title = "10 روز", Id = 3 } });
    }
}
