using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.ChangeActivation;

public sealed class ChangeIssuanceSchemesActivationValidator : AbstractValidator<ChangeIssuanceSchemesActivationCommand>
{
    public ChangeIssuanceSchemesActivationValidator(ITranslator translator)
    {
        #region IssuanceSchemesId
        RuleForEach(c => c.IssuanceSchemesId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.ISSUANCE_SCHEME_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.ISSUANCE_SCHEME_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}

