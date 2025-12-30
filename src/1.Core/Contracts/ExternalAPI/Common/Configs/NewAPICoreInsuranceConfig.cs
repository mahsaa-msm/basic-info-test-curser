namespace Master.Data.Core.Contracts.ExternalAPI.Common.Configs;

public class NewAPICoreInsuranceConfig
{
    public string BaseAddress { get; set; }
    public string Grant_Type { get; set; }
    public string Client_Id { get; set; }
    public string Client_Secret { get; set; }
    public string Scope { get; set; }
    public bool IgnoreSSL { get; set; }
    public string TokenKey { get; set; }
}
