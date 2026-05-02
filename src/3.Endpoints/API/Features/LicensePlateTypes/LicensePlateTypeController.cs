using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetById;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.LicensePlateTypes;

[Route("api/[controller]")]
[Tags("LicensePlateType - (نوع پلاک)")]
[ValidateTenantHeader]
public class LicensePlateTypeController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateLicensePlateType([FromBody] CreateLicensePlateTypeCommand command)
        => await Create<CreateLicensePlateTypeCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateLicensePlateType([FromBody] UpdateLicensePlateTypeCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeLicensePlateTypesActivation(
        [FromBody] ChangeLicensePlateTypesActivationCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchLicensePlateTypesFromSource()
        => await Edit(new FetchLicensePlateTypesFromSourceCommand());

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllLicensePlateTypes([FromQuery] GetAllLicensePlateTypeQuery query)
        => await Query<GetAllLicensePlateTypeQuery, List<LicensePlateTypeSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetLicensePlateTypeById([FromQuery] GetLicensePlateTypeByIdQuery query)
        => await Query<GetLicensePlateTypeByIdQuery, LicensePlateTypeQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllLicensePlateTypesPagedFilter(
        [FromQuery] GetAllLicensePlateTypesPagedFilterQuery query)
        => await Query<GetAllLicensePlateTypesPagedFilterQuery, PagedData<LicensePlateTypeListItemQr>>(query);
}
