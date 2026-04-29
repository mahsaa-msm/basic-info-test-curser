using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Commands;
using Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.ServiceFeatures;

public sealed class ServiceFeatureCommandRepository : BaseCommandRepository<ServiceFeature, VehicleInsuranceCommandDbContext, long>,
    IServiceFeatureCommandRepository
{
    public ServiceFeatureCommandRepository(VehicleInsuranceCommandDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<ServiceFeature>> GetAllAsync()
        => await _dbContext.ServiceFeatures.ToListAsync();

    public async Task<List<ServiceFeature>> GetByIds(List<long> serviceFeatureIds)
        => await _dbContext.ServiceFeatures.Where(c => serviceFeatureIds.Contains(c.Id)).ToListAsync();

    public async Task<List<ServiceFeature>> GetByKeyAsync(ServiceFeatureCategory key)
        => await _dbContext.ServiceFeatures.IgnoreQueryFilters().Where(c => c.Key == key).ToListAsync();
}

