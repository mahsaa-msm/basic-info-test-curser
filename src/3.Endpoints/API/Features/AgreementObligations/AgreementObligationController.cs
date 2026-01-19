using Master.Data.Core.RequestResponse.AgreementObligations.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Create;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Fetch;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Update;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.AgreementObligations;

[Route("api/[controller]")]
[Tags("AgreementObligation - (تعهدات موافقت نامه)")]
[ValidateTenantHeader]
public sealed class AgreementObligationController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateAgreementObligation([FromBody] CreateAgreementObligationCommand command)
        => await Create<CreateAgreementObligationCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateAgreementObligation([FromBody] UpdateAgreementObligationCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeAgreementObligationsActivation([FromBody] ChangeAgreementObligationsActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> FetchAgreementObligationsFromSource([FromBody] FetchAgreementObligationsFromSourceCommand commnad)
    => await Edit(commnad);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAgreementObligations([FromQuery] GetAllAgreementObligationsQuery query)
    => await Query<GetAllAgreementObligationsQuery, List<AgreementObligationSelectItemQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAgreementObligationById([FromQuery] GetAgreementObligationByIdQuery query)
        => await Query<GetAgreementObligationByIdQuery, AgreementObligationQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAgreementObligationsPagedFilter([FromQuery] GetAllAgreementObligationsPagedFilterQuery query)
        => await Query<GetAllAgreementObligationsPagedFilterQuery, PagedData<AgreementObligationListItemQr>>(query);

    #endregion
}
