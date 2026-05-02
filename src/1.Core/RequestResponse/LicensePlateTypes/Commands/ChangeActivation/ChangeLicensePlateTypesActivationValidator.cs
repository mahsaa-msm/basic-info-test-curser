using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.ChangeActivation;

public sealed class ChangeLicensePlateTypesActivationValidator : AbstractValidator<ChangeLicensePlateTypesActivationCommand>
{
    public ChangeLicensePlateTypesActivationValidator(ITranslator translator)
    {
        RuleForEach(c => c.LicensePlateTypesId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.LICENSE_PLATE_TYPE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)
            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.LICENSE_PLATE_TYPE_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
    }
}
