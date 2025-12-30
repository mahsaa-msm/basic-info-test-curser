using Master.Data.Core.Contracts.PodSsoApis.UserInfo;
using Master.Data.Endpoints.API.Infrastructor.Services.GetUserAccessKeys;
using System.Security.Claims;
using Zamin.Extensions.UsersManagement.Extensions;

namespace Master.Data.Endpoints.API.Infrastructor.Services.UserInfo;

public class ModernUserInfoService : IModernUserInfoService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ModernUserInfoService(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserAgent() => _httpContextAccessor?.HttpContext?.Request.Headers["User-Agent"];

    public string GetUserIp() => _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress.ToString();

    public string UserId() => _httpContextAccessor?.HttpContext?.User?.GetClaim(ClaimTypes.NameIdentifier) ?? "0";

    public string GetFirstName() => _httpContextAccessor?.HttpContext?.User?.GetClaim("FirstName") ?? string.Empty;

    public string GetLastName() => _httpContextAccessor?.HttpContext?.User?.GetClaim("LastName") ?? string.Empty;

    public string GetUsername() => _httpContextAccessor?.HttpContext?.User?.GetClaim(ClaimTypes.Name) ?? string.Empty;

    public bool IsCurrentUser(string userId) => string.Equals(UserId(), userId, StringComparison.OrdinalIgnoreCase);

    public bool HasAccess(string accessKey)
    {
        try
        {
            var _getUserAccessKeysService = _serviceProvider.GetRequiredService<IGetUserAccessKeysService>();
            if (_getUserAccessKeysService is null)
                return false;

            var accessKeys = _getUserAccessKeysService.ExecuteAsync(_httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault(),
                                                                    long.Parse(UserId()),
                                                                    _httpContextAccessor?.HttpContext?.User?.GetClaim("sid")).Result;

            if (accessKeys is not null && accessKeys.Count() > 0)
                return accessKeys.Any(x => string.Equals(x, accessKey, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex) { }

        return false;
    }

    public string? GetClaim(string claimType) => _httpContextAccessor?.HttpContext?.User?.GetClaim(claimType);

    public string UserIdOrDefault() => _httpContextAccessor?.HttpContext?.User?.GetClaim(ClaimTypes.NameIdentifier) ?? "0";

    public string UserIdOrDefault(string defaultValue) => _httpContextAccessor?.HttpContext?.User?.GetClaim(ClaimTypes.NameIdentifier) ?? defaultValue;
}