using Master.Data.Core.Contracts.InsuranceUnits.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.Entities;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.ApplicationService.InsuranceUnits.Commands.Update;
public sealed class UpdateInsuranceUnitHandler : CommandHandler<UpdateInsuranceUnitCommand>
{
    private readonly IInsuranceUnitCommandRepository _insuranceUnitCommandRepository;
    private readonly Dictionary<MoveDirection, Func<InsuranceUnit, UpdateInsuranceUnitCommand, Task>> _actions;

    public UpdateInsuranceUnitHandler(ZaminServices zaminServices,
                             IInsuranceUnitCommandRepository insuranceUnitCommandRepository)
        : base(zaminServices)
    {
        _insuranceUnitCommandRepository = insuranceUnitCommandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (insuranceUnit, command)
            => (await _insuranceUnitCommandRepository.GetSuperiorInsuranceUnits(insuranceUnit.Priority,
                                                                                command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (insuranceUnit, command)
            => (await _insuranceUnitCommandRepository.GetSubordinateInsuranceUnits(insuranceUnit.Priority,
                                                                                   command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateInsuranceUnitCommand command)
    {
        InsuranceUnit insuranceUnit = await _insuranceUnitCommandRepository.GetAsync(command.InsuranceUnitId);

        EntityGuard.ThrowIfNullWithLongId(insuranceUnit, ProjectTranslation.INSURANCE_UNIT);

        if (!_insuranceUnitCommandRepository.IsCreatedByCore(insuranceUnit))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = insuranceUnit.Title.Value;
            command.Name = insuranceUnit.Name.Value;
            command.Code = insuranceUnit.Code.Value;
        }

        await CheckPriority(command);

        await MoveInsuranceUnitsIfNeeded(insuranceUnit, command);

        insuranceUnit.Update(command.ToParameter());

        await _insuranceUnitCommandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateInsuranceUnitCommand command)
    {
        var nextPriority = await _insuranceUnitCommandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveInsuranceUnitsIfNeeded(InsuranceUnit current, UpdateInsuranceUnitCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateInsuranceUnitCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Name, ProjectTranslation.NAME);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _insuranceUnitCommandRepository.ExistsAsync(c => c.Id != command.InsuranceUnitId &&
                                                                   (c.Code == Code.FromString(command.Code) ||
                                                                    c.Title == Title.FromString(command.Title) ||
                                                                    c.Name == Title.FromString(command.Name))))

            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.INSURANCE_UNIT]);
    }
    #endregion
}
