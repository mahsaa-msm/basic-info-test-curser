using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Entities.Settings;

public abstract class TenantConfigSettings
{
    public abstract ConfigType Type { get; }
}
