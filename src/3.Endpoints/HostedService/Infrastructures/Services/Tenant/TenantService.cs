using Master.Data.Core.Contracts.Common.Services.Tenant;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.Tenant;

public class TenantService : ITenantService, ITransientLifetime
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantResolver _tenantResolver;

    private long? _currentTenantId;
    private Guid? _currentTenantKey;
    private bool _hasResolved = false;
    private readonly object _lock = new();

    public TenantService(IHttpContextAccessor httpContextAccessor,
                         ITenantResolver tenantResolver)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                _currentTenantId = null;
                _currentTenantKey = null;
            }
            else
            {
                _currentTenantId = _tenantResolver.ExtractTenantId(context);
                _currentTenantKey = _tenantResolver.ExtractTenantKey(context);
            }

            _hasResolved = true;
        }
    }
}
