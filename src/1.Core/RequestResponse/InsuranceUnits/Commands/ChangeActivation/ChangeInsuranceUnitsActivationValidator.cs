using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Commands.ChangeActivation;
public sealed class ChangeInsuranceUnitsActivationValidator : AbstractValidator<ChangeInsuranceUnitsActivationCommand>
{
    public ChangeInsuranceUnitsActivationValidator(ITranslator translator)
    {
        #region InsuranceUnitsId
        RuleForEach(c => c.InsuranceUnitsId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSURANCE_UNIT_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.INSURANCE_UNIT_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}