using Master.Data.Core.RequestResponse.Tenants.Commands.AddSsoConfig;
using Master.Data.Core.RequestResponse.Tenants.Commands.Create;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetById;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Tenants;

[Route("api/[controller]")]
//[ValidateBackofficeSuperAdmin]
public class TenantController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command)
        => await Create<CreateTenantCommand, long?>(command);

    [HttpPost("[action]")]
    public async Task<IActionResult> AddSsoTenantConfig([FromBody] AddSsoTenantConfigCommand command)
        => await Edit<AddSsoTenantConfigCommand>(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetTenantById([FromQuery] GetTenantByIdQuery query)
        => await Query<GetTenantByIdQuery, TenantGraphQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetTenantsPagedFilter([FromQuery] GetTenantPagedFilterQuery query)
        => await Query<GetTenantPagedFilterQuery, PagedData<TenantSelectItemQr>>(query);
    #endregion
}
