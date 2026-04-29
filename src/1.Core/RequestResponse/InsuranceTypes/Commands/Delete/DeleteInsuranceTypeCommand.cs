using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Delete;

public sealed class DeleteInsuranceTypeCommand : ICommand, IWebRequest
{
    public long InsuranceTypeId { get; set; }

    public string Path => "/Api/InsuranceType/DeleteInsuranceType";
}
