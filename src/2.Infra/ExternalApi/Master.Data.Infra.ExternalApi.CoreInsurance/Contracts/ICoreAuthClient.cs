using Master.Data.Core.Contracts.ExternalAPI.Authentication.Request;
using Refit;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;

public interface ICoreAuthClient
{
    [Post("/authentication/login")]
    Task<ApiResponse<string>> Login([Body] LoginRequestModel model);
}
