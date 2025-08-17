namespace Master.Data.Core.Contracts.Common.Options;
public sealed class SoftwareManagementOption
{
    public string BasePath { get; set; } = default!;
    public long CustomerRoleId { get; set; }
    public long ResourceId { get; set; } = default!;
    public bool IgnoreSslCheck { get; set; }
    public string CustomerIdClaimName { get; set; } = default!;
}