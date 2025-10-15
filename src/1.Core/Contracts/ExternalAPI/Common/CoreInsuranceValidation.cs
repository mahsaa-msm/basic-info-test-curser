namespace Master.Data.Core.Contracts.ExternalAPI.Common;

public class CoreInsuranceValidation
{
    public string errMsgKey { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public bool warning { get; set; }
    public string[] parameters { get; set; } = [];
}
