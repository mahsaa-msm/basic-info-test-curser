using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Tenants;

public sealed class TenantCommandRepository : BaseCommandRepository<Tenant, VehicleInsuranceCommandDbContext, long>,
        ITenantCommandRepository
{
    public TenantCommandRepository(VehicleInsuranceCommandDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Tenant>> GetByIds(List<long> tenantIds)
        => await _dbContext.Tenants.Where(c => tenantIds.Contains(c.Id))
                                   .ToListAsync();
}

