using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetAll;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetById;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Create;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Disable;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Enable;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.PullFromSource;
using Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.TravelPassengerCountTypes;

[Route("api/[controller]")]
[Tags("TravelPassengerCountType - (انواع تعداد مسافران)")]
public class TravelPassengerCountTypeController : BaseController
{
    #region Commands
    [HttpPost("CreateTravelPassengerCountType")]
    public async Task<IActionResult> CreateTravelPassengerCountType([FromBody] CreateTravelPassengerCountTypeCommand command)
      => await Create<CreateTravelPassengerCountTypeCommand, Guid>(command);

    [HttpPost("PullTravelPassengerCountTypesFromSource")]
    [AllowAnonymous]
    public async Task<IActionResult> PullTravelPassengerCountTypesFromSource([FromBody] PullTravelPassengerCountTypesFromSourceCommand command)
    {
        return await Create<PullTravelPassengerCountTypesFromSourceCommand, string>(command);
    }
    [HttpPut("DisableTravelPassengerCountType")]
    public async Task<IActionResult> DisableTravelPassengerCountType([FromBody] DisableTravelPassengerCountTypeCommand command)
    {
        return await Edit(command);
    }
    [HttpPut("EnableTravelPassengerCountType")]
    public async Task<IActionResult> EnableTravelPassengerCountType([FromBody] EnableTravelPassengerCountTypeCommand command)
    {
        return await Edit(command);
    }
    [HttpPut("UpdateTravelPassengerCountType")]
    public async Task<IActionResult> UpdateTravelPassengerCountType([FromBody] UpdateTravelPassengerCountTypeCommand command)
    {
        return await Edit(command);
    }

    #endregion

    #region Queries
    [HttpGet("GetTravelPassengerCountTypeById")]
    public async Task<IActionResult> GetTravelPassengerCountTypeById([FromQuery] GetTravelPassengerCountTypeByIdQuery query)
       => await Query<GetTravelPassengerCountTypeByIdQuery, TravelPassengerCountTypeQr?>(query);

    [HttpGet("GetAllTravelPassengerCountType")]
    public async Task<IActionResult> GetAllTravelPassengerCountType([FromQuery] GetAllTravelPassengerCountTypeQuery query)
        => await Query<GetAllTravelPassengerCountTypeQuery, List<TravelPassengerCountTypeItemQr>>(query);

    [HttpGet("GetTravelPassengerCountTypePagedFilter")]
    public async Task<IActionResult> GetTravelPassengerCountTypePagedFilter([FromQuery] GetTravelPassengerCountTypePagedFilterQuery query)
      => await Query<GetTravelPassengerCountTypePagedFilterQuery, PagedData<TravelPassengerCountTypeQr>>(query);
    #endregion
}
