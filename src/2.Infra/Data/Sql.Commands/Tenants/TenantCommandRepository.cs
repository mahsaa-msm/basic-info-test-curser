using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.Tenants;
public sealed class TenantCommandRepository : BaseCommandRepository<Tenant, MasterDataCommandDbContext, long>,
        ITenantCommandRepository
{
    public TenantCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }
}
