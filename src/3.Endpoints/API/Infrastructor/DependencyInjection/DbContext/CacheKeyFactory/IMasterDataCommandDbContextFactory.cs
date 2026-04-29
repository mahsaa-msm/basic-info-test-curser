using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public interface IVehicleInsuranceCommandDbContextFactory
{
    VehicleInsuranceCommandDbContext CreateDbContext();
}
