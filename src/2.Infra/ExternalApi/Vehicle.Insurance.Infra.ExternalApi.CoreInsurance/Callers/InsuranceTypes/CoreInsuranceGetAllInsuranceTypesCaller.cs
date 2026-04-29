using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.InsuranceTypes;
using Vehicle.Insurance.Core.RequestResponse.Common.Extensions;
using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Infra.ExternalApi.CoreInsurance.Callers.InsuranceTypes;

public sealed class CoreInsuranceGetAllInsuranceTypesCaller : ICoreInsuranceGetAllInsuranceTypesCaller, ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreInsuranceOption _coreInsuranceOption;

    public CoreInsuranceGetAllInsuranceTypesCaller(IHttpClientFactory httpClientfactory,
                                              CoreInsuranceOption coreInsuranceOption)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME);
        _coreInsuranceOption = coreInsuranceOption;
    }

    public async Task<Response<List<GetAllInsuranceTypesResponse>>> Call(GetAllInsuranceTypesRequest request)
    {
        var baseUri = new Uri(_coreInsuranceOption.BasePath ?? "");
        var response = await _httpClient.GetAsync($"{baseUri}{request.Path}");

        return await response.ToResultAsync<List<GetAllInsuranceTypesResponse>>();
    }
}

