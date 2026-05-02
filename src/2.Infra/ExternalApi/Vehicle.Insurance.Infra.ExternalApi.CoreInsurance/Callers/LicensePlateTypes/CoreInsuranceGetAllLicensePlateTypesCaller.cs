using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.LicensePlateTypes;
using Vehicle.Insurance.Core.RequestResponse.Common.Extensions;
using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.LicensePlateType.GetAll;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Infra.ExternalApi.CoreInsurance.Callers.LicensePlateTypes;

public sealed class CoreInsuranceGetAllLicensePlateTypesCaller : ICoreInsuranceGetAllLicensePlateTypesCaller,
    ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreInsuranceOption _coreInsuranceOption;

    public CoreInsuranceGetAllLicensePlateTypesCaller(IHttpClientFactory httpClientfactory,
        CoreInsuranceOption coreInsuranceOption)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME);
        _coreInsuranceOption = coreInsuranceOption;
    }

    public async Task<Response<List<GetAllLicensePlateTypesResponseItem>>> Call(
        GetAllLicensePlateTypesRequest request)
    {
        var baseUri = new Uri(_coreInsuranceOption.BasePath ?? "");
        var response = await _httpClient.GetAsync($"{baseUri}{request.Path}");
        return await response.ToResultAsync<List<GetAllLicensePlateTypesResponseItem>>();
    }
}
