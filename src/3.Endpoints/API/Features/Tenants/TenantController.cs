using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.AddSsoConfig;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.Tenants;

[Route("api/[controller]")]
//[ValidateBackofficeSuperAdmin]
public class TenantController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command)
        => await Create<CreateTenantCommand, long?>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateTenantName([FromBody] UpdateTenantNameCommand command)
    => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeTenantsActivation([FromBody] ChangeTenantsActivationCommand commnad)
        => await Edit(commnad);

    [HttpPost("[action]")]
    public async Task<IActionResult> AddSsoTenantConfig([FromBody] AddSsoTenantConfigCommand command)
        => await Edit(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetTenantById([FromQuery] GetTenantByIdQuery query)
        => await Query<GetTenantByIdQuery, TenantGraphQr?>(query);

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllTenants([FromQuery] GetAllTenantsSelectItemQuery query)
    => await Query<GetAllTenantsSelectItemQuery, List<TenantIdKeyQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetTenantsPagedFilter([FromQuery] GetTenantPagedFilterQuery query)
        => await Query<GetTenantPagedFilterQuery, PagedData<TenantSelectItemQr>>(query);
    #endregion
}

