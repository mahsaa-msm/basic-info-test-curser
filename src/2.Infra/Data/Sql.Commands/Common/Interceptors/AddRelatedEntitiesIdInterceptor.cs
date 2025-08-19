using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.Common.Services;
using Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Interceptors;

public class AddRelatedEntitiesIdInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        FillRelatedEntitiesIdProperty(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                          InterceptionResult<int> result,
                                                                          CancellationToken cancellationToken = default)
    {
        FillRelatedEntitiesIdProperty(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void FillRelatedEntitiesIdProperty(DbContextEventData eventData)
    {
        ChangeTracker changeTracker = eventData.Context.ChangeTracker;
        var tenantService = eventData.Context.GetService<ITenantService>();

        changeTracker.SetTenantIdValue(tenantService);
    }
}
