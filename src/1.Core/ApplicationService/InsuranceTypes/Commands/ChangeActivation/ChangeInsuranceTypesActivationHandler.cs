using Vehicle.Insurance.Core.Contracts.InsuranceTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.InsuranceTypes.Entities;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.InsuranceTypes.Commands.ChangeActivation;

public class ChangeInsuranceTypesActivationHandler : CommandHandler<ChangeInsuranceTypesActivationCommand>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;
    private static readonly Dictionary<bool, Action<List<InsuranceType>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeInsuranceTypesActivationHandler(ZaminServices zaminServices,
                                          IInsuranceTypeCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeInsuranceTypesActivationCommand command)
    {
        List<InsuranceType> insuranceTypes = await _commandRepository.GetByIds(command.InsuranceTypesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(insuranceTypes, ProjectTranslation.INSURANCE_TYPE);

        _actions[command.IsActive](insuranceTypes);
        await _commandRepository.CommitAsync();

        return Ok();

    }
}


