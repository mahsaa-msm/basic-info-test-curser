using Zamin.Extensions.UsersManagement.Abstractions;

namespace Vehicle.Insurance.Core.Contracts.PodSsoApis.UserInfo;

public interface IModernUserInfoService : IUserInfoService
{
    bool HasAccess(string accessKey);
}

