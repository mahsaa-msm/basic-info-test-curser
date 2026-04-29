using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Interceptors;

public class TenantQueryQueryDbIntrerceptor : IQueryExpressionInterceptor
{
    private readonly ITenantService _tenantService;

    public TenantQueryQueryDbIntrerceptor(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public Expression ProcessQuery(Expression query, IReadOnlyCollection<Type> entityTypes)
    {
        var tenantId = _tenantService.GetCurrentTenantId();
        var tenantKey = _tenantService.GetCurrentTenantKey();
        if (!tenantId.HasValue && !tenantKey.HasValue)
            return query;

        foreach (var entityType in entityTypes)
        {
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType))
            {
                if (tenantId.HasValue && tenantId > 0)
                    query = ApplyTenantIdFilter(query, entityType, (long)tenantId);

                if (tenantKey.HasValue && tenantKey != Guid.Empty)
                    query = ApplyTenantKeyFilter(query, entityType, (Guid)tenantKey);
            }
        }

        return query;
    }

    private Expression ApplyTenantIdFilter(Expression query, Type entityType, long tenantId)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var tenantIdProperty = Expression.Property(parameter, nameof(BaseTenantEntity.TenantId));
        var tenantIdConstant = Expression.Constant(tenantId);
        var equalExpression = Expression.Equal(tenantIdProperty, tenantIdConstant);
        var lambda = Expression.Lambda(equalExpression, parameter);

        var whereMethod = typeof(Queryable).GetMethods()
            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType);

        return Expression.Call(whereMethod,
                               query,
                               lambda);
    }
    private Expression ApplyTenantKeyFilter(Expression query, Type entityType, Guid tenantKey)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var tenantKeyProperty = Expression.Property(parameter, nameof(BaseTenantEntity.TenantBusinessId));


        var tenantKeyConstant = Expression.Constant(tenantKey, typeof(Guid?));
        var nullConstant = Expression.Constant(null, typeof(Guid?));
        var isNullExpression = Expression.Equal(tenantKeyProperty, nullConstant);

        var equalExpression = Expression.Equal(tenantKeyProperty, tenantKeyConstant);

        var orExpression = Expression.OrElse(isNullExpression, equalExpression);

        var lambda = Expression.Lambda(orExpression, parameter);

        var whereMethod = typeof(Queryable).GetMethods()
            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType);

        return Expression.Call(whereMethod,
                               query,
                               lambda);
    }
}

