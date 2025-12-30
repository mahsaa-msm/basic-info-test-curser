using Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelDurationTypes;
using Master.Data.Core.Contracts.ExternalAPI.CoreInsurance.TravelPassengerCountTypes;
using Refit;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;

public interface ICoreInsuranceClient
{
    
    #region GetRequests

    [Get("/anvaTedadNafarJameMosaferati/all")]
    Task<ApiResponse<TravelPassengerCountTypeResponse>> GetAllTravelPassengerCountTypes();

    [Get("/anvaModdatMosaferat/all")]
    Task<ApiResponse<TravelDurationTypesResponse>> GetAllTravelDurationTypes();
    #endregion
}


