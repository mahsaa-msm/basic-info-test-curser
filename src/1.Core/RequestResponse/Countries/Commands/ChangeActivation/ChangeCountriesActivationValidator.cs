using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Countries.Commands.ChangeActivation;

public sealed class ChangeCountriesActivationValidator : AbstractValidator<ChangeCountriesActivationCommand>
{
    public ChangeCountriesActivationValidator(ITranslator translator)
    {
        #region CountriesId
        RuleForEach(c => c.CountriesId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.COUNTRY_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.COUNTRY_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}