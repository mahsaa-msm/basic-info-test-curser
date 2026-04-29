using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Core.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Extensions;

public static class TenantIdExtensions
{
    public static void SetTenantIdValue(this ChangeTracker changeTracker, ITenantService tenantService)
    {
        var tenantId = tenantService.GetCurrentTenantId();
        var tenantKey = tenantService.GetCurrentTenantKey();

        if (tenantId is null)
            return;

        foreach (var entry in changeTracker.Entries<BaseTenantEntity<long>>())
        {
            if (entry.State == EntityState.Added && entry.Entity.TenantId < 1)
            {
                entry.Entity.TenantId = (long)tenantId;

                if (tenantKey.HasValue)
                    entry.Entity.TenantBusinessId = tenantKey;
            }

            if (entry.State == EntityState.Modified &&
                (entry.Entity.TenantId < 1 || entry.Entity.TenantBusinessId is null))
            {
                if (entry.Entity.TenantId < 1)
                    entry.Entity.TenantId = (long)tenantId;

                if (tenantKey.HasValue && entry.Entity.TenantBusinessId is null)
                    entry.Entity.TenantBusinessId = tenantKey;
            }
        }
    }
}

