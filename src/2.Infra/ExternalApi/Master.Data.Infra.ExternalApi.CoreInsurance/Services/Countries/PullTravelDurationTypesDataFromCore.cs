using Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelDurationTypes;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Resources;
using Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;
using Zamin.Core.Domain.Exceptions;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Services.Countries;

public class PullTravelDurationTypesDataFromCore : IPullTravelDurationTypesDataFromCore, ITransientLifetime
{
    private readonly ICoreInsuranceClient _coreInsuranceClient;

    public PullTravelDurationTypesDataFromCore(ICoreInsuranceClient coreInsuranceClient)
    {
        _coreInsuranceClient = coreInsuranceClient;
    }

    public async Task<List<TravelDurationTypesModel>> ExecuteAsync()
    {
        var result = await _coreInsuranceClient.GetAllTravelDurationTypes();
        if (result.Content is null)
        {
            throw new InvalidEntityStateException(ProjectValidationError.ERROR_IN_GET_DATA_FROM_CORE_INSURANCE_API,
                 ProjectTranslation.CORE_API_TOKEN);
        }
        return result.Content?.itemList;
    }
}