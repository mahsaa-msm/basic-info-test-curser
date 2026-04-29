using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Delete;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;
using Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.ParrotTranslations;

[Route("api/[controller]")]
[Tags("ParrotTranslation - (ترجمه ها)")]
[ValidateTenantHeader]
public class ParrotTranslationController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateParrotTranslationCommand command)
        => await base.Create(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateParrotTranslationCommand command)
        => await Edit(command);

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete([FromBody] DeleteParrotTranslationCommand command)
        => await base.Delete(command);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById([FromQuery] GetParrotTranslationByIdQuery query)
        => await Query<GetParrotTranslationByIdQuery, ParrotTranslationItemQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetPagedFilter([FromQuery] GetParrotTranslationPagedFilterQuery query)
        => await Query<GetParrotTranslationPagedFilterQuery, PagedData<ParrotTranslationItemQr>>(query);

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] GetAllParrotTranslationQuery query)
        => await Query<GetAllParrotTranslationQuery, ParrotTranslationQr>(query);
    #endregion
}

