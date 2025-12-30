using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetAll;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetById;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetPagedFilter;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Create;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Disable;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Enable;
using Master.Data.Core.RequestResponse.TravelDurationTypess.PullFromSource;
using Master.Data.Core.RequestResponse.TravelDurationTypess.Update;
using Master.Data.Core.RequestResponse.VehicleTypes.Queries.CommonResults;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.TravelDurations;

[Route("api/[controller]")]
[Tags("TravelDuration - (مدت سفر)")]
[ValidateTenantHeader]
public class TravelDurationController : BaseController
{
    #region Commands
    [HttpPost("CreateTravelDurationType")]
    public async Task<IActionResult> CreateTravelDurationTypes([FromBody] CreateTravelDurationTypesCommand command)
      => await Create<CreateTravelDurationTypesCommand, Guid>(command);

    [HttpPost("PullTravelDurationTypesFromSource")]
    [AllowAnonymous]
    public async Task<IActionResult> PullTravelDurationTypessFromSource([FromBody] PullTravelDurationTypessFromSourceCommand command)
    {
        return await Create<PullTravelDurationTypessFromSourceCommand, string>(command);
    }
    [HttpPut("DisableTravelDurationType")]
    public async Task<IActionResult> DisableTravelDurationTypes([FromBody] DisableTravelDurationTypesCommand command)
    {
        return await Edit(command);
    }
    [HttpPut("EnableTravelDurationType")]
    public async Task<IActionResult> EnableTravelDurationTypes([FromBody] EnableTravelDurationTypesCommand command)
    {
        return await Edit(command);
    }
    [HttpPut("UpdateTravelDurationType")]
    public async Task<IActionResult> UpdateTravelDurationTypes([FromBody] UpdateTravelDurationTypesCommand command)
    {
        return await Edit(command);
    }

    #endregion

    #region Queries
    [HttpGet("GetTravelDurationTypeById")]
    public async Task<IActionResult> GetTravelDurationTypesById([FromQuery] GetTravelDurationTypesByIdQuery query)
       => await Query<GetTravelDurationTypesByIdQuery, TravelDurationTypesQr?>(query);

    [AllowAnonymous]
    [HttpGet("GetAllTravelDuration")]
    public async Task<IActionResult> GetAllTravelDurationTypes([FromQuery] GetAllTravelDurationTypesQuery query)
    //=> await Query<GetAllTravelDurationTypesQuery, List<TravelDurationTypesItemQr>>(query);
    {
        return Ok(new[] {
             new TravelDurationTypesItemQr { Id= 1, Title= "6 تا 10 روز", CoreId=5},
             new TravelDurationTypesItemQr { Id= 2, Title= "16 تا 20 روز",CoreId=21 },
             new TravelDurationTypesItemQr { Id= 3, Title= "11 تا 15 روز", CoreId=6},
             new TravelDurationTypesItemQr { Id= 4, Title= "1 تا 5 روز", CoreId=4},
        });
    }

    [HttpGet("GetTravelDurationTypesPagedFilter")]
    public async Task<IActionResult> GetTravelDurationTypesPagedFilter([FromQuery] GetTravelDurationTypesPagedFilterQuery query)
      => await Query<GetTravelDurationTypesPagedFilterQuery, PagedData<TravelDurationTypesQr>>(query);
    #endregion
}
