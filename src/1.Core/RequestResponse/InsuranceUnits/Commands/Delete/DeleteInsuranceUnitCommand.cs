using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.Delete;

public sealed class DeleteInsuranceUnitCommand : ICommand, IWebRequest
{
    public long InsuranceUnitId { get; set; }

    public string Path => "/Api/InsuranceUnit/DeleteInsuranceUnit";
}
