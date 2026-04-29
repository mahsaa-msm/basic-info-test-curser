using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.ChangeActivation;

public sealed class ChangeAgreementObligationsActivationValidator : AbstractValidator<ChangeAgreementObligationsActivationCommand>
{
    public ChangeAgreementObligationsActivationValidator(ITranslator translator)
    {
        #region AgreementObligationsId
        RuleForEach(c => c.AgreementObligationsId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.AGREEMENT_OBLIGATION_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.AGREEMENT_OBLIGATION_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}
