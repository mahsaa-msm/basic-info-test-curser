namespace Master.Data.Core.Contracts.Common.Services;
public interface ITenantService
{
    long? GetCurrentTenantId();
    Guid? GetCurrentTenantKey();
    void SetCurrentTenant(long? tenantId, Guid? tenantKey);
}