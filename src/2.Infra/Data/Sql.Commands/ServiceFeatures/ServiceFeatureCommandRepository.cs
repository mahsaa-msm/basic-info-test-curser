using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.ServiceFeatures;

public sealed class ServiceFeatureCommandRepository : BaseCommandRepository<ServiceFeature, MasterDataCommandDbContext, long>,
    IServiceFeatureCommandRepository
{
    public ServiceFeatureCommandRepository(MasterDataCommandDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<ServiceFeature>> GetAllAsync()
        => await _dbContext.ServiceFeatures.ToListAsync();

    public async Task<List<ServiceFeature>> GetByIds(List<long> serviceFeatureIds)
        => await _dbContext.ServiceFeatures.Where(c => serviceFeatureIds.Contains(c.Id)).ToListAsync();
}
