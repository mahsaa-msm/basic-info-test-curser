using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.Tenants.Entities.Settings;

public abstract class TenantConfigSettings
{
    public abstract ConfigType Type { get; }
}

