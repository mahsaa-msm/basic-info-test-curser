namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Common;

public sealed class CoreInsuranceValidation
{
    public string errMsgKey { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public bool warning { get; set; }
    public List<string> parameters { get; set; } = new();
}

