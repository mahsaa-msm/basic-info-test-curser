using System.Text.Json;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.Tenants.Parameters;

public sealed record CreateTenantConfigParameters(ConfigType ConfigType,
                                                  JsonDocument Settings);
