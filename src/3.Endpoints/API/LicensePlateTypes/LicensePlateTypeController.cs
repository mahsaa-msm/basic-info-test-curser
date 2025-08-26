using Master.Data.Core.RequestResponse.Cities.Queries.CommanResults;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.LicensePlateType;

[Route("api/[controller]")]
[Tags("LicensePlateType - (انوع پلاک)")]
[ValidateTenantHeader]
public class LicensePlateTypeController : BaseController
{
    [HttpGet("GetAllLicensePlateType")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllLicensePlateType([FromQuery] GetAllLicensePlateTypeQuery query)
    {
        return Ok(new[] { new LicensePlateTypeItemQr { Title = "شخصی", Id = 1 }, new LicensePlateTypeItemQr { Title = "سیاسی", Id = 2 }, new LicensePlateTypeItemQr { Title = "گذر موقت", Id = 3 } });
    }
}
