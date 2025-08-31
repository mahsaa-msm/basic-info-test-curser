using Master.Data.Core.RequestResponse.TravelDurations.Queries.CommanResults;
using Master.Data.Core.RequestResponse.TravelDurations.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.TravelDurations;

[Route("api/[controller]")]
[Tags("TravelDuration - (مدت سفر)")]
[ValidateTenantHeader]
public class TravelDurationController : BaseController
{
    [HttpGet("GetAllTravelDuration")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllTravelDuration([FromQuery] GetAllTravelDurationQuery query)
    {
        return Ok(new[] { new TravelDurationItemQr { Title = "تا 5 روز", Id = 4 }, new TravelDurationItemQr { Title = "تا 20 روز", Id = 6 }, new TravelDurationItemQr { Title = "تا 10 روز", Id = 5 } });
    }
}