using Vehicle.Insurance.Core.Contracts.AgreementObligations.Commands;
using Vehicle.Insurance.Core.Domain.AgreementObligations.Entities;
using Vehicle.Insurance.Core.Domain.AgreementObligations.Parameters;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.AgreementObligations.Commands.Update;

public sealed class UpdateAgreementObligationHandler : CommandHandler<UpdateAgreementObligationCommand>
{
    private readonly IAgreementObligationCommandRepository _agreementObligationCommandRepository;
    private readonly Dictionary<MoveDirection, Func<AgreementObligation, UpdateAgreementObligationCommand, Task>> _actions;

    public UpdateAgreementObligationHandler(ZaminServices zaminServices,
                                            IAgreementObligationCommandRepository agreementObligationCommandRepository)
        : base(zaminServices)
    {
        _agreementObligationCommandRepository = agreementObligationCommandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (agreementObligation, command) =>
                (await _agreementObligationCommandRepository.GetSuperiorAgreementObligations(agreementObligation.Priority, command.Priority))
                    .ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (agreementObligation, command) =>
                (await _agreementObligationCommandRepository.GetSubordinateAgreementObligations(agreementObligation.Priority, command.Priority))
                    .ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateAgreementObligationCommand command)
    {
        AgreementObligation agreementObligation = await _agreementObligationCommandRepository.GetAsync(command.AgreementObligationId);

        EntityGuard.ThrowIfNullWithLongId(agreementObligation, ProjectTranslation.AGREEMENT_OBLIGATION);

        if (!_agreementObligationCommandRepository.IsCreatedByCore(agreementObligation))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = agreementObligation.Title.Value;
            command.Code = agreementObligation.Code.Value;
        }

        await CheckPriority(command);

        await MoveCitiesIfNeeded(agreementObligation, command);

        agreementObligation.Update(new UpdateAgreementObligationParameter(command.Title,
                                                                          command.DisplayTitle,
                                                                          command.AgreementCoreId,
                                                                          command.Code,
                                                                          command.StartDateUtc,
                                                                          command.EndDateUtc,
                                                                          command.PrepaymentPercentage,
                                                                          command.FirstInstallmentDeadline,
                                                                          command.InstallmentsCount,
                                                                          command.InstallmentInterval,
                                                                          command.AgreementObligationNumber,
                                                                          command.AgreementNumber,
                                                                          command.InsuranceTypeCoreId,
                                                                          command.SalesType,
                                                                          command.Priority));

        await _agreementObligationCommandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateAgreementObligationCommand command)
    {
        var nextPriority = await _agreementObligationCommandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveCitiesIfNeeded(AgreementObligation current, UpdateAgreementObligationCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateAgreementObligationCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _agreementObligationCommandRepository.ExistsAsync(c => c.Id != command.AgreementObligationId &&
                                                                         (c.Code == Code.FromString(command.Code) || c.Title == DIPTitle.FromString(command.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.AGREEMENT_OBLIGATION]);
    }
    #endregion
}

