using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.ChangeActivation;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Create;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Upsert;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetByKey;
using Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Models;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs;

[Route("api/[controller]")]
[Tags("PatternCatalog - (الگو ها)")]
public sealed class PatternCatalogController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePatternCatalog([FromBody] CreatePatternCatalogViewModel viewModel)
        => await Create<CreatePatternCatalogCommand, long>(viewModel.ToCommand());

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdatePatternCatalog([FromBody] UpdatePatternCatalogViewModel viewModel)
        => await Edit<UpdatePatternCatalogCommand>(viewModel.ToCommand());

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangePatternCatalogsActivation([FromBody] ChangePatternCatalogsActivationCommand commnad)
        => await Edit(commnad);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpsertInsurancePolicyPatternCatalog([FromBody] UpsertInsurancePolicyPatternCatalogViewModel viewModel)
    => await Edit<UpsertPatternCatalogCommand, long?>(viewModel.ToCommand());
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetPatternCatalogById([FromQuery] GetPatternCatalogByIdQuery query)
        => await Query<GetPatternCatalogByIdQuery, PatternCatalogQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetPatternCatalogByKey([FromQuery] GetPatternCatalogByKeyQuery query)
        => await Query<GetPatternCatalogByKeyQuery, PatternCatalogQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllPatternCatalogsPagedFilter([FromQuery] GetAllPatternCatalogsPagedFilterQuery query)
        => await Query<GetAllPatternCatalogsPagedFilterQuery, PagedData<PatternCatalogQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllPatternCatalogs([FromQuery] GetAllPatternCatalogsQuery query)
        => await Query<GetAllPatternCatalogsQuery, List<PatternCatalogQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllInsurancePolicyPatterns([FromQuery] GetAllInsurancePolicyPatternCatalogsViewModel viewModel)
        => await Query<GetAllPatternCatalogsQuery, List<PatternCatalogQr>>(viewModel.ToQuery());
    #endregion
}

