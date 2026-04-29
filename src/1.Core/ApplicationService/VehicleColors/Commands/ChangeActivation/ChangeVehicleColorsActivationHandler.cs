using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Commands.ChangeActivation;

public class ChangeVehicleColorsActivationHandler : CommandHandler<ChangeVehicleColorsActivationCommand>
{
    private readonly IVehicleColorCommandRepository _commandRepository;
    private static readonly Dictionary<bool, Action<List<VehicleColor>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeVehicleColorsActivationHandler(ZaminServices zaminServices,
                                          IVehicleColorCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeVehicleColorsActivationCommand command)
    {
        List<VehicleColor> vehicleColors = await _commandRepository.GetByIds(command.VehicleColorsId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(vehicleColors, ProjectTranslation.VEHICLE_COLOR);

        _actions[command.IsActive](vehicleColors);
        await _commandRepository.CommitAsync();

        return Ok();

    }
}



