using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Commands.ChangeActivation;

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

