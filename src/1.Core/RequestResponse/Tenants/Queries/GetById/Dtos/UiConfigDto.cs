namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

public sealed class UiConfigDto : ConfigDto
{
    public string Theme { get; set; } = string.Empty;
}