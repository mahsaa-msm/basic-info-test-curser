using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Create;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Update;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAll;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.IssuanceSchemes;

[Route("api/[controller]")]
[Tags("IssuanceScheme - (طرح صدور)")]
[ValidateTenantHeader]
public class IssuanceSchemeController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateIssuanceScheme([FromBody] CreateIssuanceSchemeCommand command)
        => await Create<CreateIssuanceSchemeCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateIssuanceScheme([FromBody] UpdateIssuanceSchemeCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeIssuanceSchemesActivation([FromBody] ChangeIssuanceSchemesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchIssuanceSchemesFromSource([FromBody] FetchIssuanceSchemesFromSourceCommand commnad)
    => await Edit(commnad);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllIssuanceSchemes([FromQuery] GetAllIssuanceSchemeQuery query)
    => await Query<GetAllIssuanceSchemeQuery, List<IssuanceSchemeSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetIssuanceSchemeById([FromQuery] GetIssuanceSchemeByIdQuery query)
        => await Query<GetIssuanceSchemeByIdQuery, IssuanceSchemeQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllIssuanceSchemesPagedFilter([FromQuery] GetAllIssuanceSchemesPagedFilterQuery query)
        => await Query<GetAllIssuanceSchemesPagedFilterQuery, PagedData<IssuanceSchemeListItemQr>>(query);

    #endregion
}
