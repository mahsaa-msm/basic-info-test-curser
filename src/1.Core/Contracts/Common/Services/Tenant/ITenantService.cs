namespace Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;

public interface ITenantService
{
    long? GetCurrentTenantId();
    Guid? GetCurrentTenantKey();

    void SetCurrentTenant(long? tenantId, Guid? tenantKey);
}
