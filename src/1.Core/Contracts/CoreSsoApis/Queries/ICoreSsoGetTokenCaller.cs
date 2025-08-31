using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;

namespace Master.Data.Core.Contracts.CoreSsoApis.Queries;
public interface ICoreSsoGetTokenCaller
{
    Task<Response<CoreSsoGetTokenResponse>> Call(CoreSsoGetTokenRequest request);
}
