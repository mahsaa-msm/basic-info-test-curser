namespace Master.Data.Core.Contracts.Common.Options;
public sealed class CoreInsuranceOption
{
    public string BasePath { get; set; } = default!;
    public bool IgnoreSslCheck { get; set; }
}
