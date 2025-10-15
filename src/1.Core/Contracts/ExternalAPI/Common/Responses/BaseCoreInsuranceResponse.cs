using Master.Data.Core.Contracts.ExternalAPI.Common;

namespace Master.Data.Core.Contracts.ExternalAPI.Common.Responses;

public class BaseCoreInsuranceResponse
{
    public bool IsTimeoutExceeded { get; set; }
    public bool HasError { get; set; } = true;
    public bool Success { get; set; }
    public CoreInsuranceResponseStatus Status { get; set; } = CoreInsuranceResponseStatus.Unknown;
    public List<CoreInsuranceValidation> Validations { get; set; } = [];

    public bool IsSucceed()
    {
        return Success;
    }

    public bool IsSucceedWithAnyValidation()
    {
        if (Success)
        {
            return !Validations.Any();
        }
        return false;
    }

    public List<string> GetValidationNames(bool showWarning = false)
    {
        if (!showWarning)
        {
            return (from c in Validations
                    where !c.warning
                    select c.name).ToList();
        }

        return Validations.Select((c) => c.name).ToList();
    }
}
