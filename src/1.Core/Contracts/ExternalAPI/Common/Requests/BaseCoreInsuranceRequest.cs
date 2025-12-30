using Master.Data.Core.Contracts.ExternalAPI.Common;

namespace Master.Data.Core.Contracts.ExternalAPI.Common.Requests;

public class BaseCoreInsuranceRequest
{
    public bool HasError { get; set; }
    public bool Success { get; set; }
    public List<CoreInsuranceValidation> Validations { get; set; } = [];
}
