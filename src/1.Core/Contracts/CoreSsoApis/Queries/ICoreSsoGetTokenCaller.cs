using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;

namespace Vehicle.Insurance.Core.Contracts.CoreSsoApis.Queries;

public interface ICoreSsoGetTokenCaller
{
    Task<Response<CoreSsoGetTokenResponse>> Call(CoreSsoGetTokenRequest request);
}

