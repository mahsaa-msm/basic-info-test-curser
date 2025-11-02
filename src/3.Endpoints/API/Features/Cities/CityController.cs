using Master.Data.Core.RequestResponse.Cities.Queries.CommanResults;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.Cities;

[Route("api/[controller]")]
[Tags("City - (شهر ها)")]
[ValidateTenantHeader]
public class CityController : BaseController
{
    [HttpGet("GetAllCity")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCity([FromQuery] GetAllCityQuery query)
    {
        return Ok(new[] {
            new CityItemQr { Title = "تهران", Id = 1 }        ,
            new CityItemQr { Title = "شیراز", Id = 2 },
            new CityItemQr { Title = "تبریز", Id = 3 },
            new CityItemQr { Title = "مشهد", Id = 4 },
            new CityItemQr { Title = "اصفهان", Id = 5 },
            new CityItemQr { Title = "کیش", Id = 6 },
        });
    }
}

