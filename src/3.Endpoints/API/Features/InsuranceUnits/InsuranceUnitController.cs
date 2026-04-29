using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.InsuranceUnits;

[Route("api/[controller]")]
[Tags("InsuranceUnit - (واحد بیمه)")]
[ValidateTenantHeader]
public sealed class InsuranceUnitController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateDIPInsuranceUnit([FromBody] CreateInsuranceUnitCommand command)
        => await Create<CreateInsuranceUnitCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateDIPInsuranceUnit([FromBody] UpdateInsuranceUnitCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeDIPInsuranceUnitsActivation([FromBody] ChangeInsuranceUnitsActivationCommand commnad)
        => await Edit(commnad);

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteDIPInsuranceUnit([FromBody] DeleteInsuranceUnitCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetDIPInsuranceUnitById([FromQuery] GetInsuranceUnitByIdQuery query)
        => await Query<GetInsuranceUnitByIdQuery, InsuranceUnitQr>(query);

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDIPInsuranceUnitInArea([FromQuery] GetAllInsuranceUnitsInAreaQuery query)
    => await Query<GetAllInsuranceUnitsInAreaQuery, List<InsuranceUnitMapItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDIPInsuranceUnitsPagedFilter([FromQuery] GetAllInsuranceUnitsPagedFilterQuery query)
        => await Query<GetAllInsuranceUnitsPagedFilterQuery, PagedData<InsuranceUnitListItemQr>>(query);

    #endregion
}

