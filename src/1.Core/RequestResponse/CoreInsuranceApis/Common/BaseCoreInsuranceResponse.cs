namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Common;

public class BaseCoreInsuranceResponse<ContentType>
{
    public List<CoreInsuranceValidation> validations { get; set; } = new();
    public ContentType content { get; set; }
}

public class BaseCoreInsuranceResponse
{
    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    public bool hasError { get; set; }
    public List<CoreInsuranceValidation> validations { get; set; } = new();
}
