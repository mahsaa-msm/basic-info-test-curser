using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Commands.Update;

public class UpdateInsuranceTypeHandler : CommandHandler<UpdateInsuranceTypeCommand>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<InsuranceType, UpdateInsuranceTypeCommand, Task>> _actions;

    public UpdateInsuranceTypeHandler(ZaminServices zaminServices,
                                IInsuranceTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (county, command) => (await _commandRepository.GetSuperiorInsuranceTypes(county.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (county, command) => (await _commandRepository.GetSubordinateInsuranceTypes(county.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateInsuranceTypeCommand command)
    {
        InsuranceType insuranceType = await _commandRepository.GetAsync(command.InsuranceTypeId);

        EntityGuard.ThrowIfNullWithLongId(insuranceType, ProjectTranslation.INSURANCE_TYPE);

        if (!_commandRepository.IsCreatedByCore(insuranceType))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = insuranceType.Title.Value;
            command.Code = insuranceType.Code.Value;
        }

        await CheckPriority(command);

        await MoveInsuranceTypesIfNeeded(insuranceType, command);

        insuranceType.Update(command.ToParameter());

        await _commandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateInsuranceTypeCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveInsuranceTypesIfNeeded(InsuranceType current, UpdateInsuranceTypeCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateInsuranceTypeCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.InsuranceTypeId &&
                                                      (c.Code == Code.FromString(command.Code) || c.Title == Title.FromString(command.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.INSURANCE_TYPE]);
    }
    #endregion
}
