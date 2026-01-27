using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.CoreInsuranceApis.IssuanceSchemes;
using Master.Data.Core.RequestResponse.Common.Extensions;
using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
using Master.Data.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Callers.IssuanceSchemes;
public sealed class CoreInsuranceGetAllIssuanceSchemesCaller : ICoreInsuranceGetAllIssuanceSchemesCaller, ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreInsuranceOption _coreInsuranceOption;

    public CoreInsuranceGetAllIssuanceSchemesCaller(IHttpClientFactory httpClientfactory,
                                              CoreInsuranceOption coreInsuranceOption)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME);
        _coreInsuranceOption = coreInsuranceOption;
    }

    public async Task<Response<List<GetAllIssuanceSchemesResponse>>> Call(GetAllIssuanceSchemesRequest request)
    {
        var baseUri = new Uri(_coreInsuranceOption.BasePath ?? "");
        var response = await _httpClient.GetAsync($"{baseUri}{request.Path}");

        return await response.ToResultAsync<List<GetAllIssuanceSchemesResponse>>();
    }
}
