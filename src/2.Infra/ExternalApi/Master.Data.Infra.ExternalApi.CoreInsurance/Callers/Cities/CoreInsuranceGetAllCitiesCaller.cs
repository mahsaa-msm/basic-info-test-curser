using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.CoreInsuranceApis.Cities;
using Master.Data.Core.RequestResponse.Common.Extensions;
using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;
using Master.Data.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Callers.Cities;

public sealed class CoreInsuranceGetAllCitiesCaller : ICoreInsuranceGetAllCitiesCaller, ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreInsuranceOption _coreInsuranceOption;

    public CoreInsuranceGetAllCitiesCaller(IHttpClientFactory httpClientfactory,
                                           CoreInsuranceOption coreInsuranceOption)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME);
        _coreInsuranceOption = coreInsuranceOption;
    }

    public async Task<Response<List<GetAllCitiesResponse>>> Call(GetAllCitiesRequest request)
    {
        var baseUri = new Uri(_coreInsuranceOption.BasePath ?? "");
        var response = await _httpClient.GetAsync($"{baseUri}{request.Path}");

        return await response.ToResultAsync<List<GetAllCitiesResponse>>();
    }
}
