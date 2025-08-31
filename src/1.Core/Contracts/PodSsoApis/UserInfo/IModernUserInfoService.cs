using Zamin.Extensions.UsersManagement.Abstractions;

namespace Master.Data.Core.Contracts.PodSsoApis.UserInfo;
public interface IModernUserInfoService : IUserInfoService
{
    bool HasAccess(string accessKey);
}
