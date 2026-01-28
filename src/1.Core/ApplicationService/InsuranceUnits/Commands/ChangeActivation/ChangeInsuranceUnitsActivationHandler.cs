using Master.Data.Core.Contracts.InsuranceUnits.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.InsuranceUnits.Entities;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceUnits.Commands.ChangeActivation;

public sealed class ChangeInsuranceUnitsActivationHandler : CommandHandler<ChangeInsuranceUnitsActivationCommand>
{
    private readonly IInsuranceUnitCommandRepository _commandRepository;
    private static readonly Dictionary<bool, Action<List<InsuranceUnit>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeInsuranceUnitsActivationHandler(ZaminServices zaminServices,
                                                 IInsuranceUnitCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeInsuranceUnitsActivationCommand command)
    {
        List<InsuranceUnit> insuranceUnits = await _commandRepository.GetByIds(command.InsuranceUnitsId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(insuranceUnits, ProjectTranslation.INSURANCE_UNIT);

        _actions[command.IsActive](insuranceUnits);
        await _commandRepository.CommitAsync();

        return Ok();
    }
}
