using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.InsuranceTypes;

[Route("api/[controller]")]
[Tags("InsuranceType - (انواع بیمه)")]
[ValidateTenantHeader]
public class InsuranceTypeController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateDIPInsuranceType([FromBody] CreateInsuranceTypeCommand command)
        => await Create<CreateInsuranceTypeCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateDIPInsuranceType([FromBody] UpdateInsuranceTypeCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeDIPInsuranceTypesActivation([FromBody] ChangeInsuranceTypesActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchDIPInsuranceTypesFromSource()
    => await Edit(new FetchInsuranceTypesFromSourceCommand());

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteDIPInsuranceType([FromBody] DeleteInsuranceTypeCommand command)
        => await Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDIPInsuranceTypes([FromQuery] GetAllInsuranceTypeQuery query)
    => await Query<GetAllInsuranceTypeQuery, List<InsuranceTypeSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetDIPInsuranceTypeById([FromQuery] GetInsuranceTypeByIdQuery query)
        => await Query<GetInsuranceTypeByIdQuery, InsuranceTypeQr>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDIPInsuranceTypesPagedFilter([FromQuery] GetAllInsuranceTypesPagedFilterQuery query)
        => await Query<GetAllInsuranceTypesPagedFilterQuery, PagedData<InsuranceTypeListItemQr>>(query);

    #endregion
}

