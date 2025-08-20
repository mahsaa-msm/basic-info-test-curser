using Master.Data.Core.Contracts.Common.Services;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Core.ApplicationService.Tenants;
public class TenantService : ITenantService, IScopeLifetime
{
    private long? _currentTenantId;
    private Guid? _currentTenantKey;

    public long? GetCurrentTenantId() => _currentTenantId;
    public Guid? GetCurrentTenantKey() => _currentTenantKey;

    public void SetCurrentTenant(long? tenantId, Guid? tenantKey)
    {
        _currentTenantId = tenantId;
        _currentTenantKey = tenantKey;
    }
}