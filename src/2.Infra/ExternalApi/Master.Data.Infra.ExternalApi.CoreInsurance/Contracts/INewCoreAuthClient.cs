using Master.Data.Core.Contracts.ExternalAPI.Authentication.Response;
using Refit;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;

public interface INewCoreAuthClient
{
    [Post("/oauth/token")]
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    Task<ApiResponse<GetTokenSSOResponseModel>> Login([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> formData);
}
