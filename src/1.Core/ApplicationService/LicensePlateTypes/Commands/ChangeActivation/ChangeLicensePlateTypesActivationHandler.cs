using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Commands.ChangeActivation;

public sealed class ChangeLicensePlateTypesActivationHandler : CommandHandler<ChangeLicensePlateTypesActivationCommand>
{
    private readonly ILicensePlateTypeCommandRepository _commandRepository;

    private static readonly Dictionary<bool, Action<List<LicensePlateType>>> Actions = new()
    {
        [true] = list => list.ForEach(e => e.Active()),
        [false] = list => list.ForEach(e => e.Deactive()),
    };

    public ChangeLicensePlateTypesActivationHandler(ZaminServices zaminServices,
        ILicensePlateTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeLicensePlateTypesActivationCommand command)
    {
        var list = await _commandRepository.GetByIds(command.LicensePlateTypesId);
        EntityGuard.ThrowIfListIsEmptyWithLongId(list, ProjectTranslation.LICENSE_PLATE_TYPE);
        Actions[command.IsActive](list);
        await _commandRepository.CommitAsync();
        return Ok();
    }
}
