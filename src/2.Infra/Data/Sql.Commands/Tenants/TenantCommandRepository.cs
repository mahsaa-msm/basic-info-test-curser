using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.Tenants;

public sealed class TenantCommandRepository : BaseCommandRepository<Tenant, MasterDataCommandDbContext, long>,
        ITenantCommandRepository
{
    public TenantCommandRepository(MasterDataCommandDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Tenant>> GetByIds(List<long> tenantIds)
        => await _dbContext.Tenants.Where(c => tenantIds.Contains(c.Id))
                                   .ToListAsync();
}
