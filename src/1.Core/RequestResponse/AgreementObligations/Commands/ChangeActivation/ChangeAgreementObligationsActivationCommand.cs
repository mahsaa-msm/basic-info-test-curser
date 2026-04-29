using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.ChangeActivation;

public sealed class ChangeAgreementObligationsActivationCommand : ICommand, IWebRequest
{
    public List<long> AgreementObligationsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/AgreementObligation/ChangeAgreementObligationsActivation";
}
