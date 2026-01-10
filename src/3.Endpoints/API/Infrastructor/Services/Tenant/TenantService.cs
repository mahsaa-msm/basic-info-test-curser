using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant;

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGrpcServerCallContextAccessor _grpcContextAccessor;
    private readonly ITenantResolver _tenantResolver;

    private long? _currentTenantId;
    private Guid? _currentTenantKey;
    private bool _hasResolved = false;
    private readonly object _lock = new();

    public TenantService(IHttpContextAccessor httpContextAccessor,
                         IGrpcServerCallContextAccessor grpcContextAccessor,
                         ITenantResolver tenantResolver)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _grpcContextAccessor = grpcContextAccessor ?? throw new ArgumentNullException(nameof(grpcContextAccessor));
        _tenantResolver = tenantResolver ?? throw new ArgumentNullException(nameof(tenantResolver));
    }

    public long? GetCurrentTenantId()
    {
        if (!_hasResolved)
            EnsureTenantResolved();
        return _currentTenantId;
    }

    public Guid? GetCurrentTenantKey()
    {
        if (!_hasResolved)
            EnsureTenantResolved();
        return _currentTenantKey;
    }

    public void SetCurrentTenant(long? tenantId, Guid? tenantKey)
    {
        lock (_lock)
        {
            _currentTenantId = tenantId;
            _currentTenantKey = tenantKey;
            _hasResolved = true;
        }
    }

    private void EnsureTenantResolved()
    {
        lock (_lock)
        {
            if (_hasResolved) return;

            var httpContext = _httpContextAccessor.HttpContext;
            var grpcContext = _grpcContextAccessor.ServerCallContext;
            if (httpContext == null && grpcContext == null)
            {
                _currentTenantId = _tenantResolver.ExtractTenantId();
                _currentTenantKey = _tenantResolver.ExtractTenantKey();
            }
            else if (httpContext != null && grpcContext == null)
            {
                _currentTenantId = _tenantResolver.ExtractTenantIdHttp(httpContext);
                _currentTenantKey = _tenantResolver.ExtractTenantKeyHttp(httpContext);
            }
            else if (httpContext == null && grpcContext != null)
            {
                _currentTenantId = _tenantResolver.ExtractTenantIdGrpc(grpcContext);
                _currentTenantKey = _tenantResolver.ExtractTenantKeyGrpc(grpcContext);
            }

            _hasResolved = true;
        }
    }
}
