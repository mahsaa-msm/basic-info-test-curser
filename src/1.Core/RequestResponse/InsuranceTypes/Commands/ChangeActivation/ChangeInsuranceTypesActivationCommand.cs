using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.ChangeActivation;

public sealed class ChangeInsuranceTypesActivationCommand : ICommand, IWebRequest
{
    public List<long> InsuranceTypesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/InsuranceType/ChangeInsuranceTypesActivation";
}
