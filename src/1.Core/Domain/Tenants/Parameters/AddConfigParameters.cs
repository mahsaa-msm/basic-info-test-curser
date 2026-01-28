using System.Text.Json;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Parameters;

public sealed record AddConfigParameters(ConfigType ConfigType,
                                         JsonDocument Settings);