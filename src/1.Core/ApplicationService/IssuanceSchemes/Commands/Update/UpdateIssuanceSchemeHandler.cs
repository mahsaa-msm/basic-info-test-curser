using Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.IssuanceSchemes.Commands.Update;

public class UpdateIssuanceSchemeHandler : CommandHandler<UpdateIssuanceSchemeCommand>
{
    private readonly IIssuanceSchemeCommandRepository _commandRepository;
    private readonly Dictionary<MoveDirection, Func<IssuanceScheme, UpdateIssuanceSchemeCommand, Task>> _actions;

    public UpdateIssuanceSchemeHandler(ZaminServices zaminServices,
                                IIssuanceSchemeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (county, command) => (await _commandRepository.GetSuperiorIssuanceSchemes(county.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (county, command) => (await _commandRepository.GetSubordinateIssuanceSchemes(county.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateIssuanceSchemeCommand command)
    {
        IssuanceScheme issuanceScheme = await _commandRepository.GetAsync(command.IssuanceSchemeId);

        EntityGuard.ThrowIfNullWithLongId(issuanceScheme, ProjectTranslation.ISSUANCE_SCHEME);

        if (!_commandRepository.IsCreatedByCore(issuanceScheme))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = issuanceScheme.Title.Value;
            command.Code = issuanceScheme.Code.Value;
            command.FromStartDateUtc = issuanceScheme.FromStartDateUtc;
            command.ToStartDateUtc = issuanceScheme.ToStartDateUtc;
            command.FromIssueDateUtc = issuanceScheme.FromIssueDateUtc;
            command.ToIssueDateUtc = issuanceScheme.ToIssueDateUtc;
            command.InsuranceTypeCoreId = issuanceScheme.InsuranceTypeCoreId.Value;
            command.AdjustmentType = issuanceScheme.AdjustmentType;
            command.AdjustmentPercent = issuanceScheme.AdjustmentPercent?.Value;
        }

        await CheckPriority(command);

        await MoveIssuanceSchemesIfNeeded(issuanceScheme, command);

        issuanceScheme.Update(command.ToParameter());

        await _commandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateIssuanceSchemeCommand command)
    {
        var nextPriority = await _commandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveIssuanceSchemesIfNeeded(IssuanceScheme current, UpdateIssuanceSchemeCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateIssuanceSchemeCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _commandRepository.ExistsAsync(c => c.Id != command.IssuanceSchemeId &&
                                                      (c.Code == Code.FromString(command.Code) || c.Title == DIPTitle.FromString(command.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.ISSUANCE_SCHEME]);
    }
    #endregion
}

