using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public interface IVehicleInsuranceCommandDbContextFactory
{
    VehicleInsuranceCommandDbContext CreateDbContext();
}