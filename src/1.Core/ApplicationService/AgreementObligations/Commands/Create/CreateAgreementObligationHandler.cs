using Master.Data.Core.Contracts.AgreementObligations.Commands;
using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.AgreementObligations.Parameters;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Commands.Create;

public sealed class CreateAgreementObligationHandler : CommandHandler<CreateAgreementObligationCommand, long>
{
    private readonly IAgreementObligationCommandRepository _agreementObligationCommandRepository;

    public CreateAgreementObligationHandler(ZaminServices zaminServices,
                                IAgreementObligationCommandRepository agreementObligationCommandRepository)
        : base(zaminServices)
    {
        _agreementObligationCommandRepository = agreementObligationCommandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateAgreementObligationCommand command)
    {
        var isDuplicateAgreementObligation = await _agreementObligationCommandRepository
            .ExistsAsync(e => e.Title == Title.FromString(command.Title) ||
                              e.Code == Code.FromString(command.Code) ||
                              e.CoreId == CoreId.FromString(command.CoreId));

        if (isDuplicateAgreementObligation)
            throw new InvalidEntityStateException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.AGREEMENT_OBLIGATION]);


        long nextPriority = await _agreementObligationCommandRepository.GetNextPriority();

        AgreementObligation agreementObligation = await Create(command, nextPriority);

        await _agreementObligationCommandRepository.CommitAsync();

        return Ok(agreementObligation.Id);
    }

    #region Methods
    private async Task<AgreementObligation> Create(CreateAgreementObligationCommand command, long nextPriority)
    {
        var agreementObligation = AgreementObligation.Create(new CreateAgreementObligationParameter(command.Title,
                                                                                                    command.DisplayTitle,
                                                                                                    command.CoreId,
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
                                                                                                    nextPriority));

        await _agreementObligationCommandRepository.InsertAsync(agreementObligation);

        return agreementObligation;
    }
    #endregion
}