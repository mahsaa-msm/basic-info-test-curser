using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Commands.Update;

public sealed class UpdateLicensePlateTypeHandler : CommandHandler<UpdateLicensePlateTypeCommand>
{
    private readonly ILicensePlateTypeCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<LicensePlateType, UpdateLicensePlateTypeCommand, Task>> _actions;

    public UpdateLicensePlateTypeHandler(ZaminServices zaminServices,
        ILicensePlateTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new Dictionary<MoveDirection, Func<LicensePlateType, UpdateLicensePlateTypeCommand, Task>>
        {
            [MoveDirection.Up] = async (entity, command) =>
                (await _commandRepository.GetSuperiorLicensePlateTypes(entity.Priority, command.Priority))
                .ForEach(e => e.PushDown()),
            [MoveDirection.Down] = async (entity, command) =>
                (await _commandRepository.GetSubordinateLicensePlateTypes(entity.Priority, command.Priority))
                .ForEach(e => e.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateLicensePlateTypeCommand command)
    {
        var entity = await _commandRepository.GetAsync(command.LicensePlateTypeId);
        EntityGuard.ThrowIfNullWithLongId(entity, ProjectTranslation.LICENSE_PLATE_TYPE);

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

    private async Task CheckPriority(UpdateLicensePlateTypeCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }

    private async Task MoveIfNeeded(LicensePlateType current, UpdateLicensePlateTypeCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
            await _actions[moveDirection](current, command);
    }

    private async Task ValidateTitle(UpdateLicensePlateTypeCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.LicensePlateTypeId
                && DIPTitle.FromString(command.Title).Equals(c.Title)))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[
                ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.LICENSE_PLATE_TYPE]);
    }
}
