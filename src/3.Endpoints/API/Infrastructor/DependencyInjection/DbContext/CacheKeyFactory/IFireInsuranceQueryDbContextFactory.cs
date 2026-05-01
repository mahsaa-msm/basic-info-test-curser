using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public interface IVehicleInsuranceQueryDbContextFactory
{
    VehicleInsuranceQueryDbContext CreateDbContext();
}