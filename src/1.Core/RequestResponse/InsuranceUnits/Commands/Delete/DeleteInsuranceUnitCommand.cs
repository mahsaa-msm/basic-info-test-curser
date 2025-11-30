using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Delete;
public sealed class DeleteInsuranceUnitCommand : ICommand, IWebRequest
{
    public long InsuranceUnitId { get; set; }

    public string Path => "/Api/InsuranceUnit/DeleteInsuranceUnit";
}