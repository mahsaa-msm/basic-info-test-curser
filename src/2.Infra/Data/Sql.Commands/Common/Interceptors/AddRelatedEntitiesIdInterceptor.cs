using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Interceptors;

public class AddRelatedEntitiesIdInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        TryFillRelatedEntitiesIdProperty(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                          InterceptionResult<int> result,
                                                                          CancellationToken cancellationToken = default)
    {
        TryFillRelatedEntitiesIdProperty(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void TryFillRelatedEntitiesIdProperty(DbContextEventData eventData)
    {
        try
        {
            var context = eventData?.Context;
            if (context == null)
                return;

            var changeTracker = context.ChangeTracker;
            if (changeTracker == null)
                return;

            var tenant = context.GetService<ITenantService>();
            if (tenant == null)
                return;

            changeTracker.SetTenantIdValue(tenant);
        }
        catch (Exception ex) when (ex is NullReferenceException ||
                                   ex is InvalidOperationException)
        {
            // لاگ کردن خطا در صورت نیاز
        }
    }
}

