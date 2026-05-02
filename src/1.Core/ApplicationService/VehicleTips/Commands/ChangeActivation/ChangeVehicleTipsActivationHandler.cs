using Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Commands.ChangeActivation;

public sealed class ChangeVehicleTipsActivationHandler : CommandHandler<ChangeVehicleTipsActivationCommand>
{
    private readonly IVehicleTipCommandRepository _commandRepository;

    private static readonly Dictionary<bool, Action<List<VehicleTip>>> Actions = new()
    {
        [true] = list => list.ForEach(e => e.Active()),
        [false] = list => list.ForEach(e => e.Deactive()),
    };

    public ChangeVehicleTipsActivationHandler(ZaminServices zaminServices,
        IVehicleTipCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeVehicleTipsActivationCommand command)
    {
        var list = await _commandRepository.GetByIds(command.VehicleTipsId);
        EntityGuard.ThrowIfListIsEmptyWithLongId(list, ProjectTranslation.VEHICLE_TIP);
        Actions[command.IsActive](list);
        await _commandRepository.CommitAsync();
        return Ok();
    }
}
