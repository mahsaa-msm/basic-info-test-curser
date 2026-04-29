using Vehicle.Insurance.Core.Contracts.AgreementObligations.Commands;
using Vehicle.Insurance.Core.Domain.AgreementObligations.Entities;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.AgreementObligations.Commands.ChangeActivation;

public sealed class ChangeAgreementObligationsActivationHandler : CommandHandler<ChangeAgreementObligationsActivationCommand>
{
    private readonly IAgreementObligationCommandRepository _agreementObligationCommandRepository;
    private static readonly Dictionary<bool, Action<List<AgreementObligation>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };

    public ChangeAgreementObligationsActivationHandler(ZaminServices zaminServices,
                                                       IAgreementObligationCommandRepository agreementObligationCommandRepository)
        : base(zaminServices)
    {
        _agreementObligationCommandRepository = agreementObligationCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeAgreementObligationsActivationCommand command)
    {
        List<AgreementObligation> agreementObligations = await _agreementObligationCommandRepository.GetByIds(command.AgreementObligationsId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(agreementObligations, ProjectTranslation.AGREEMENT_OBLIGATION);

        _actions[command.IsActive](agreementObligations);
        await _agreementObligationCommandRepository.CommitAsync();

        return Ok();
    }
}

