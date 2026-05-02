using Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Commands.Update;

public sealed class UpdateVehicleBrandHandler : CommandHandler<UpdateVehicleBrandCommand>
{
    private readonly IVehicleBrandCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<VehicleBrand, UpdateVehicleBrandCommand, Task>> _actions;

    public UpdateVehicleBrandHandler(ZaminServices zaminServices,
        IVehicleBrandCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new Dictionary<MoveDirection, Func<VehicleBrand, UpdateVehicleBrandCommand, Task>>
        {
            [MoveDirection.Up] = async (entity, command) =>
                (await _commandRepository.GetSuperiorVehicleBrands(entity.Priority, command.Priority))
                .ForEach(e => e.PushDown()),
            [MoveDirection.Down] = async (entity, command) =>
                (await _commandRepository.GetSubordinateVehicleBrands(entity.Priority, command.Priority))
                .ForEach(e => e.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateVehicleBrandCommand command)
    {
        var entity = await _commandRepository.GetAsync(command.VehicleBrandId);
        EntityGuard.ThrowIfNullWithLongId(entity, ProjectTranslation.VEHICLE_BRAND);

        if (!_commandRepository.IsCreatedByCore(entity))
            await ValidateTitle(command);
        else
        {
            command.Title = entity.Title.Value;
            command.DisplayTitle = entity.DisplayTitle.Value;
        }

        await CheckPriority(command);
        await MoveIfNeeded(entity, command);
        entity.Update(command.ToParameter());
        await _commandRepository.CommitAsync();
        return Ok();
    }

    private async Task CheckPriority(UpdateVehicleBrandCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }

    private async Task MoveIfNeeded(VehicleBrand current, UpdateVehicleBrandCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
            await _actions[moveDirection](current, command);
    }

    private async Task ValidateTitle(UpdateVehicleBrandCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.VehicleBrandId
                && DIPTitle.FromString(command.Title).Equals(c.Title)))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[
                ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.VEHICLE_BRAND]);
    }
}
