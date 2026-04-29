using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.GetAccessList;

public interface IGetAccessListService : ITransientLifetime
{
    Task<List<AccessModel>> ExecuteAsync();
}

public class AccessModel
{
    public string NamePath { get; set; }
    public string AccessKey { get; set; }
    public bool HasNeedToAccess { get; set; }
}

public class GetAccessListServiceException : Exception
{
    public GetAccessListServiceExceptionType _getAccessListServiceExceptionType;
    public GetAccessListServiceException(GetAccessListServiceExceptionType getAccessListServiceExceptionType, string message) : base(message)
    {
        _getAccessListServiceExceptionType = getAccessListServiceExceptionType;
    }
}
public enum GetAccessListServiceExceptionType
{
    GetTokenFaild
}

