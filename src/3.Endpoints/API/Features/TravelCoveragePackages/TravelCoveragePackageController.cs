using Master.Data.Core.RequestResponse.TravelCoveragePackages.Queries.CommanResults;
using Master.Data.Core.RequestResponse.TravelCoveragePackages.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.TravelCoveragePackages;

[Route("api/[controller]")]
[Tags("TravelCoveragePackage - (بسته بیمه)")]
[ValidateTenantHeader]
public class TravelCoveragePackageController : BaseController
{
    [HttpGet("GetAllTravelCoveragePackage")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllTravelCoveragePackage([FromQuery] GetAllTravelCoveragePackageQuery query)
    {
        return Ok(new[] {
            new TravelCoveragePackageItemQr { Title = "بیمه کامل", Id = 1 ,  Description = "پوشش خانه ، مسافران و خودرو" },
            new TravelCoveragePackageItemQr { Title = "خانه و مسافران", Id = 21 ,Description = "آتش سوزی ، حادثه" }
        });
    }
}
