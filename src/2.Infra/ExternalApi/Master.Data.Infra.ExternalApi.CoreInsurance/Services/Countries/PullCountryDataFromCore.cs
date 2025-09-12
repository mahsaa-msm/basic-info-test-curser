using Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelPassengerCountTypes;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Commands;
using Master.Data.Core.Resources;
using Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;
using Zamin.Core.Domain.Exceptions;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Services.Countries;

public class PullTravelPassengerCountTypeDataFromCore : IPullTravelPassengerCountTypeDataFromCore, ITransientLifetime
{
    private readonly ICoreInsuranceClient _coreInsuranceClient;

    public PullTravelPassengerCountTypeDataFromCore(ICoreInsuranceClient coreInsuranceClient)
    {
        _coreInsuranceClient = coreInsuranceClient;
    }

    public async Task<List<TravelPassengerCountTypeModel>> ExecuteAsync()
    {
        var result = await _coreInsuranceClient.GetAllTravelPassengerCountTypes();
        if (result.Content is null)
        {
            throw new InvalidEntityStateException(ProjectValidationError.ERROR_IN_GET_DATA_FROM_CORE_INSURANCE_API,
                 ProjectTranslation.CORE_API_TOKEN);
        }
        return result.Content?.itemList;
    }
}