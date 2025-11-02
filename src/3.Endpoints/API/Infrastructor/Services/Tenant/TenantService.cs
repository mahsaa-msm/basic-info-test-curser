using Master.Data.Core.Contracts.Common.Services.Tenant;
using Microsoft.AspNetCore.Http;

namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant;

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantResolver _tenantResolver;

    private long? _currentTenantId;
    private Guid? _currentTenantKey;

    public TenantService(
        IHttpContextAccessor httpContextAccessor,
        ITenantResolver tenantResolver)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantResolver = tenantResolver;
    }

    public long? GetCurrentTenantId()
    {
        EnsureTenantResolved();
        return _currentTenantId;
    }

    public Guid? GetCurrentTenantKey()
    {
        EnsureTenantResolved();
        return _currentTenantKey;
    }

    public void SetCurrentTenant(long? tenantId, Guid? tenantKey)
    {
        _currentTenantId = tenantId;
        _currentTenantKey = tenantKey;
    }

    private void EnsureTenantResolved()
    {
        if (_currentTenantId.HasValue || _currentTenantKey.HasValue)
            return;

        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            SetCurrentTenant(null, null);
            return;
        }

        var tenantId = _tenantResolver.ExtractTenantId(context);
        var tenantKey = _tenantResolver.ExtractTenantKey(context);
        SetCurrentTenant(tenantId, tenantKey);
    }
}
