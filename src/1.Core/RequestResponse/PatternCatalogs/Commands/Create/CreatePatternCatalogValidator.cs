using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Create;

public sealed class CreatePatternCatalogValidator : AbstractValidator<CreatePatternCatalogCommand>
{
    public CreatePatternCatalogValidator(ITranslator translator)
    {
        #region Key
        RuleFor(command => command.Key)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PATTERN_KEY])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(patternKey => patternKey.Length >= ProjectConsts.PATTERN_KEY_MIN_LENGTH && patternKey.Length <= ProjectConsts.PATTERN_KEY_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.PATTERN_KEY,
                                     ProjectConsts.PATTERN_KEY_MIN_LENGTH.ToString(),
                                     ProjectConsts.PATTERN_KEY_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Pattern
        RuleFor(command => command.Pattern)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.REGEX_EXPRESSION])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .MaximumLength(ProjectConsts.PATTERN_MAX_LENGTH)
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_MAX_LENGTH],
                                        ProjectTranslation.REGEX_EXPRESSION,
                                        ProjectConsts.PATTERN_MAX_LENGTH))
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Type
        RuleFor(command => command.Type)
             .Cascade(CascadeMode.Stop)
             .IsInEnum()
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID],
                                        translator[ProjectTranslation.REGEX_EXPRESSION]))
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion

        #region Description
        When(c => !string.IsNullOrEmpty(c.Description), () =>
        {
            RuleFor(command => command.Description)
            .Cascade(CascadeMode.Stop)
            .Must(description => description.Length >= ProjectConsts.DESCRIPTION_MIN_LENGTH && description.Length <= ProjectConsts.DESCRIPTION_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DESCRIPTION,
                                    ProjectConsts.DESCRIPTION_MIN_LENGTH.ToString(),
                                    ProjectConsts.DESCRIPTION_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
        #endregion
    }
}
