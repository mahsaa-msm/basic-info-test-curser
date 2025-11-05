using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.GetUserAccessKeys;

public interface IGetUserAccessKeysService : IScopeLifetime
{
    Task<List<string>> ExecuteAsync(string token, long userId, string sessionId);
}
public class UserInfoResult
{
    public long sub { get; set; }
    public List<string> access_Key { get; set; } = new();
}