using Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Commands.Update;

public sealed class UpdateVehicleTipHandler : CommandHandler<UpdateVehicleTipCommand>
{
    private readonly IVehicleTipCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<VehicleTip, UpdateVehicleTipCommand, Task>> _actions;

    public UpdateVehicleTipHandler(ZaminServices zaminServices,
        IVehicleTipCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new Dictionary<MoveDirection, Func<VehicleTip, UpdateVehicleTipCommand, Task>>
        {
            [MoveDirection.Up] = async (entity, command) =>
                (await _commandRepository.GetSuperiorVehicleTips(entity.Priority, command.Priority))
                .ForEach(e => e.PushDown()),
            [MoveDirection.Down] = async (entity, command) =>
                (await _commandRepository.GetSubordinateVehicleTips(entity.Priority, command.Priority))
                .ForEach(e => e.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateVehicleTipCommand command)
    {
        var entity = await _commandRepository.GetAsync(command.VehicleTipId);
        EntityGuard.ThrowIfNullWithLongId(entity, ProjectTranslation.VEHICLE_TIP);

        if (!_commandRepository.IsCreatedByCore(entity))
            await ValidateTitle(command);
        else
        {
            command.Title = entity.Title.Value;
            command.DisplayTitle = entity.DisplayTitle.Value;
            command.BrandCoreId = entity.BrandCoreId.Value;
            command.VehicleTypeCoreId = entity.VehicleTypeCoreId.Value;
            command.VehicleSystemCoreId = entity.VehicleSystemCoreId.Value;
        }

        await CheckPriority(command);
        await MoveIfNeeded(entity, command);
        entity.Update(command.ToParameter());
        await _commandRepository.CommitAsync();
        return Ok();
    }

    private async Task CheckPriority(UpdateVehicleTipCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }

    private async Task MoveIfNeeded(VehicleTip current, UpdateVehicleTipCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
            await _actions[moveDirection](current, command);
    }

    private async Task ValidateTitle(UpdateVehicleTipCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.VehicleTipId
                && DIPTitle.FromString(command.Title).Equals(c.Title)))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[
                ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.VEHICLE_TIP]);
    }
}
