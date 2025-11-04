using Master.Data.Core.RequestResponse.CoreInsuranceApis.Common;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.CommonResults;
public abstract class BaseListItemResponse
{
    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    public bool hasError { get; set; }
    public List<CoreInsuranceValidation> validations { get; set; } = new();
}
