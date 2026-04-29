using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

public class ConfigDto
{
    public long ConfigId { get; set; }
    public ConfigType Type { get; set; }
    public DateTime LastModifiedDateUtc { get; set; }
}

