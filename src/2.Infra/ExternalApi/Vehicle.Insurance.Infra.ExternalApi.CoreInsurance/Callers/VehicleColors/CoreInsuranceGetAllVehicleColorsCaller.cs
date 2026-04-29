using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.VehicleColors;
using Vehicle.Insurance.Core.RequestResponse.Common.Extensions;
using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleColor.GetAll;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Infra.ExternalApi.CoreInsurance.Callers.VehicleColors;

public sealed class CoreInsuranceGetAllVehicleColorsCaller : ICoreInsuranceGetAllVehicleColorsCaller, ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreInsuranceOption _coreInsuranceOption;

    public CoreInsuranceGetAllVehicleColorsCaller(IHttpClientFactory httpClientfactory,
                                                  CoreInsuranceOption coreInsuranceOption)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME);
        _coreInsuranceOption = coreInsuranceOption;
    }

    public async Task<Response<List<GetAllVehicleColorsResponse>>> Call(GetAllVehicleColorsRequest request)
    {
        var baseUri = new Uri(_coreInsuranceOption.BasePath ?? "");
        var response = await _httpClient.GetAsync($"{baseUri}{request.Path}");

        return await response.ToResultAsync<List<GetAllVehicleColorsResponse>>();
    }
}

