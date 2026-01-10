using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.ChangeActivation;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Create;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Update;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAll;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.Attributes;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.RequestResponse.Queries;
using Zamin.EndPoints.Web.Controllers;

namespace Master.Data.Endpoints.API.Features.ServiceFeatures;

[Route("api/[controller]")]
[Tags("ServiceFeatures - (قابلیت های سرویس ها)")]
[ValidateTenantHeader]
public sealed class ServiceFeatureController : BaseController
{
    #region Commands
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateServiceFeature([FromBody] CreateServiceFeatureCommand command)
        => await Create<CreateServiceFeatureCommand, long>(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateServiceFeature([FromBody] UpdateServiceFeatureCommand command)
        => await Edit(command);

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeServiceFeaturesActivation([FromBody] ChangeServiceFeaturesActivationCommand commnad)
        => await Edit(commnad);
    #endregion

    #region Queries
    [HttpGet("[action]")]
    public async Task<IActionResult> GetServiceFeatureById([FromQuery] GetServiceFeatureByIdQuery query)
        => await Query<GetServiceFeatureByIdQuery, ServiceFeatureQr?>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllServiceFeatures([FromQuery] GetAllServiceFeaturesQuery query)
    => await Query<GetAllServiceFeaturesQuery, List<ServiceFeatureQr>>(query);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllServiceFeaturesPagedFilter([FromQuery] GetAllServiceFeaturesPagedFilterQuery query)
        => await Query<GetAllServiceFeaturesPagedFilterQuery, PagedData<ServiceFeatureQr>>(query);
    #endregion
}
