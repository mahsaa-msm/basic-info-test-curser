using Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.IssuanceSchemes.Commands.ChangeActivation;

public class ChangeIssuanceSchemesActivationHandler : CommandHandler<ChangeIssuanceSchemesActivationCommand>
{
    private readonly IIssuanceSchemeCommandRepository _commandRepository;
    private static readonly Dictionary<bool, Action<List<IssuanceScheme>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeIssuanceSchemesActivationHandler(ZaminServices zaminServices,
                                          IIssuanceSchemeCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeIssuanceSchemesActivationCommand command)
    {
        List<IssuanceScheme> issuanceSchemes = await _commandRepository.GetByIds(command.IssuanceSchemesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(issuanceSchemes, ProjectTranslation.ISSUANCE_SCHEME);

        _actions[command.IsActive](issuanceSchemes);
        await _commandRepository.CommitAsync();

        return Ok();

    }
}


