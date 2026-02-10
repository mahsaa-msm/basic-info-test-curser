using System.Text.Json;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Parameters;

public sealed record CreateTenantConfigParameters(ConfigType ConfigType,
                                                  JsonDocument Settings);