using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Create;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Delete;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Update;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.InsuranceUnits;

[Route("api/[controller]")]
[Tags("InsuranceUnit - (واحد بیمه)")]
[ValidateTenantHeader]
public sealed class InsuranceUnitController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateInsuranceUnit([FromBody] CreateInsuranceUnitCommand command)
        => await Create<CreateInsuranceUnitCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateInsuranceUnit([FromBody] UpdateInsuranceUnitCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeInsuranceUnitsActivation([FromBody] ChangeInsuranceUnitsActivationCommand commnad)
        => await Edit(commnad);

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteInsuranceUnit([FromBody] DeleteInsuranceUnitCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetInsuranceUnitById([FromQuery] GetInsuranceUnitByIdQuery query)
        => await Query<GetInsuranceUnitByIdQuery, InsuranceUnitQr>(query);

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllInsuranceUnitInArea([FromQuery] GetAllInsuranceUnitsInAreaQuery query)
    => await Query<GetAllInsuranceUnitsInAreaQuery, List<InsuranceUnitMapItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllInsuranceUnitsPagedFilter([FromQuery] GetAllInsuranceUnitsPagedFilterQuery query)
        => await Query<GetAllInsuranceUnitsPagedFilterQuery, PagedData<InsuranceUnitListItemQr>>(query);

    #endregion
}
