using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.ChangeActivation;

public sealed class ChangeInsuranceUnitsActivationCommand : ICommand, IWebRequest
{
    public List<long> InsuranceUnitsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/InsuranceUnit/ChangeInsuranceUnitsActivation";
}
