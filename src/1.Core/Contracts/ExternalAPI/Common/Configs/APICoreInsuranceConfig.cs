namespace Master.Data.Core.Contracts.ExternalAPI.Common.Configs;

public class APICoreInsuranceConfig
{
    public string TokenKey { get; set; }
    public string BaseAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public byte RetryTimeOutCount { get; set; }
    public string LoginTokenName { get; set; }
    public bool IgnoreSSL { get; set; }
}
