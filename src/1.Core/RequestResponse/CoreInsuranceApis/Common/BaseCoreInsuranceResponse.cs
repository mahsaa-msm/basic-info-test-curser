namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Common;
public class BaseCoreInsuranceResponse<ContentType>
{
    public List<CoreInsuranceValidation> validations { get; set; } = new();
    public ContentType content { get; set; }
}
