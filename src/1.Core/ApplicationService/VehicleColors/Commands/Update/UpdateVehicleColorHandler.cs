using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Commands.Update;

public class UpdateVehicleColorHandler : CommandHandler<UpdateVehicleColorCommand>
{
    private readonly IVehicleColorCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<VehicleColor, UpdateVehicleColorCommand, Task>> _actions;

    public UpdateVehicleColorHandler(ZaminServices zaminServices,
                                IVehicleColorCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (county, command) => (await _commandRepository.GetSuperiorVehicleColors(county.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (county, command) => (await _commandRepository.GetSubordinateVehicleColors(county.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateVehicleColorCommand command)
    {
        VehicleColor vehicleColor = await _commandRepository.GetAsync(command.VehicleColorId);

        EntityGuard.ThrowIfNullWithLongId(vehicleColor, ProjectTranslation.VEHICLE_COLOR);

        if (!_commandRepository.IsCreatedByCore(vehicleColor))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = vehicleColor.Title.Value;
            command.ColorHash = vehicleColor.ColorHash.Value;
        }

        await CheckPriority(command);

        await MoveVehicleColorsIfNeeded(vehicleColor, command);

        vehicleColor.Update(command.ToParameter());

        await _commandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateVehicleColorCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveVehicleColorsIfNeeded(VehicleColor current, UpdateVehicleColorCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateVehicleColorCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.ColorHash, ProjectTranslation.COLOR_HASH);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.VehicleColorId &&
                                                      (ColorHash.FromString(command.ColorHash).Equals(c.ColorHash) || DIPTitle.FromString(command.Title).Equals(c.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.VEHICLE_COLOR]);
    }
    #endregion
}



