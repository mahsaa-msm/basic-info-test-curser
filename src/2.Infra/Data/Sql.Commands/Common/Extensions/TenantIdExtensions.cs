using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Core.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
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
        }
    }
}
