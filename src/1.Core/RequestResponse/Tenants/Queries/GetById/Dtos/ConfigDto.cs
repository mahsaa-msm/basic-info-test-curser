using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

public class ConfigDto
{
    public long ConfigId { get; set; }
    public ConfigType Type { get; set; }
    public DateTime LastModifiedDateUtc { get; set; }
}
